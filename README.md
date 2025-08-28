# RegexWrap

![RegexWrap Icon](RegexWrap/icon.png)

> Uma API fluente para construir expressões regulares de forma legível e encadeada.

RegexWrap é uma biblioteca .NET que permite criar expressões regulares de forma fluente, encadeada e muito mais legível. Ideal para quem quer evitar a complexidade e a verbosidade das regex tradicionais.

---

## ✨ Instalação

Instale via NuGet:

```bash
dotnet add package RegexWrap --version 1.0.0-preview
```

## 🚀 Exemplo Rápido

```CSharp
using RegexWrap;

var regex = RegexBuilder.StartPattern()
    .StartOfLine()
    .JustLetters().OneOrMore()
    .WhiteSpace()
    .JustNumbers().Repeat(2, 4)
    .EndOfLine()
    .ReturnAsRegex();

bool isMatch = regex.IsMatch("Nome 1234"); // true
```

### 🔤 Caracteres

| Método         | Descrição                  |
|----------------|----------------------------|
| `JustNumbers()`| Dígitos (`\d`)             |
| `JustLetters()`| Letras (`[a-zA-Z]`)        |
| `WhiteSpace()` | Espaços em branco (`\s`)   |
| `AnyChar()`    | Qualquer caractere (`.`)   |
| `Lit(string)`  | Literal escapado           |

### 🔁 Repetições

| Método             | Descrição                                      |
|--------------------|------------------------------------------------|
| `Repeat(n)`        | Repete exatamente `n` vezes (`{n}`)            |
| `Repeat(min, max)` | Repete entre `min` e `max` vezes (`{min,max}`) |
| `Optional()`       | Opcional (`?`)                                 |
| `OneOrMore()`      | Um ou mais (`+`)                               |
| `ZeroOrMore()`     | Zero ou mais (`*`)                             |

### 🧩 Agrupamentos

| Método                        | Descrição                                 |
|-------------------------------|--------------------------------------------|
| `Group(Action<RegexWrap>)`    | Grupo capturador (`(...)`)                 |
| `NonCapturingGroup(Action<>)` | Grupo não capturador (`(?:...)`)           |
| `Or()`                        | Alternância entre padrões (`|`)            |


### 📍 Âncoras

| Método         | Descrição                      |
|----------------|--------------------------------|
| `StartOfLine()`| Início da linha (`^`)          |
| `EndOfLine()`  | Fim da linha (`$`)             |


### 🧪 Exemplos Avançados

#### Validar um e-mail simples

```csharp
var emailRegex = RegexBuilder.StartPattern()
    .StartOfLine()
    .Group(r => r.JustLetters().OneOrMore())
    .Lit("@")
    .Group(r => r.JustLetters().OneOrMore())
    .Lit(".")
    .Group(r => r.JustLetters().Repeat(2, 3))
    .EndOfLine()
    .ReturnAsRegex();

bool isValid = emailRegex.IsMatch("teste@email.com"); // true
```

#### Número de telefone (formato: `(XX) XXXX-XXXX`)

```csharp
var phoneRegex = RegexBuilder.StartPattern()
    .Lit("(").JustNumbers().Repeat(2).Lit(")")
    .WhiteSpace()
    .JustNumbers().Repeat(4)
    .Lit("-")
    .JustNumbers().Repeat(4)
    .ReturnAsRegex();

bool isValid = phoneRegex.IsMatch("(41) 9999-1234"); // true
```
