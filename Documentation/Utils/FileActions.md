# FormatStrings

<details>
<summary><b> ReplaceNewLineForEnvironmentNewLine </b></summary>

Permite realizar replace da quebra de linha desejada pelo padr�o de quebra de linha do ambiente de execu��o do teste.

Exemplo de uso:

```c#
  FormatStrings.ReplaceNewLineForEnvironmentNewLine("texto\r\nQuebrado");
  FormatStrings.ReplaceNewLineForEnvironmentNewLine("texto\nQuebrado", "\n");
```

</details>

<details>
<summary><b> ReturnEnvironmentNewLine </b></summary>

Busca o valor que o ambiente usa para Nova linha.

Exemplo de uso:

```c#
  FormatStrings.ReturnEnvironmentNewLine();
```
</details>

