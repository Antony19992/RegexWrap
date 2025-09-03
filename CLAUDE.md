# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

RegexWrap is a .NET 9 library that provides a fluent API for building regular expressions in a readable, chainable manner. Version 2.0 features a completely refactored architecture following SOLID principles, design patterns, and optimized for .NET 9 performance.

## Architecture

RegexWrap 2.0 follows a component-based architecture with separation of concerns:

### Core Structure
- **Component System**: Each regex element is a separate `IRegexComponent`
- **Builder Pattern**: `FluentRegexBuilder` orchestrates component assembly
- **Factory Pattern**: `RegexComponentFactory` creates components
- **Strategy Pattern**: Components implement different regex building strategies

### Key Interfaces
- `IRegexComponent`: Base interface for all regex elements
- `IPatternBuilder`: Builds patterns from components
- `IRegexFactory`: Creates regex instances with different options

### Component Categories
- **Characters**: `NumberComponent`, `LetterComponent`, `WhitespaceComponent`, `AnyCharComponent`
- **Quantifiers**: `RepeatComponent`, `OptionalComponent`, `OneOrMoreComponent`, `ZeroOrMoreComponent`
- **Groups**: `GroupComponent`, `NonCapturingGroupComponent`
- **Anchors**: `StartOfLineComponent`, `EndOfLineComponent`
- **Special**: `LiteralComponent`, `AlternationComponent`, `SearchValuesComponent` (.NET 9)

## Development Commands

### Build and Package
```bash
# Build the solution (.NET 9 required)
dotnet build

# Build in release mode (automatically generates NuGet package)
dotnet build -c Release

# Run examples
dotnet run --project RegexWrap.Examples

# Run tests (requires .NET 9 runtime)
dotnet test
```

### Testing
- Unit tests in `RegexWrap.Tests/`
- Performance benchmarks included
- Backward compatibility tests for legacy `RegexBuilder`

### NuGet Package Management
- Package version: 2.0.0
- Package auto-generates on Release builds
- Enhanced description includes SOLID principles and .NET 9 optimization

## Core Components

### FluentRegexBuilder Class (New Architecture)
- **Entry point**: `FluentRegexBuilder.StartPattern()`
- **Component-based**: Uses `IRegexComponent` instances internally
- **Performance optimized**: .NET 9 features like `CollectionsMarshal.AsSpan()`
- **Method categories** (same API as v1 for compatibility):
  - Character classes: `JustNumbers()`, `JustLetters()`, `WhiteSpace()`, `AnyChar()`
  - Literals: `Lit(string)` - automatically escapes special characters
  - Quantifiers: `Repeat()`, `Optional()`, `OneOrMore()`, `ZeroOrMore()`
  - Grouping: `Group()`, `NonCapturingGroup()` (supports named groups)
  - Anchors: `StartOfLine()`, `EndOfLine()`
  - Alternation: `Or()`

### Legacy RegexBuilder (Backward Compatibility)
- **Deprecated but functional**: Marked with `[Obsolete]`
- **Delegates to new architecture**: Uses `FluentRegexBuilder` internally
- **Migration path**: Existing code works without changes

### .NET 9 Specific Features
- **Source Generators**: Custom `RegexGeneratedAttribute` that uses native `GeneratedRegex`
- **SearchValues Integration**: High-performance character set matching
- **CollectionsMarshal**: Optimized list iteration in `PatternBuilder`
- **AOT Compatibility**: Full support for Native AOT compilation

## Development Notes

- **Target Framework**: .NET 9.0
- **Nullable Reference Types**: Enabled throughout
- **Performance**: Optimized with .NET 9 features (SearchValues, CollectionsMarshal)
- **Architecture**: SOLID principles, dependency injection ready
- **Extensibility**: New components can be added without modifying existing code
- **Testing**: Comprehensive unit tests with xUnit
- **Examples**: Complete examples in `RegexWrap.Examples/`

### Migration from v1.x to v2.0
1. **No breaking changes**: Existing code continues to work
2. **Deprecation warnings**: Legacy `RegexBuilder` shows obsolete warnings
3. **Recommended migration**: Replace `RegexBuilder.StartPattern()` with `FluentRegexBuilder.StartPattern()`
4. **Performance benefits**: New architecture is optimized for .NET 9

### Repository Structure
```
RegexWrap/
├── Core/                    # Interfaces and base classes
├── Components/              # Component implementations
│   ├── Characters/          # Character class components
│   ├── Quantifiers/         # Quantifier components  
│   ├── Groups/              # Group components
│   └── Anchors/             # Anchor components
├── Factories/               # Factory implementations
├── Generators/              # Source generator support
├── FluentRegexBuilder.cs    # Main fluent API
└── RegexWrap.cs            # Legacy API (backward compatibility)
```