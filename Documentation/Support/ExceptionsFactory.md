# ExceptionsFactory

<details>
<summary><b> Depreciated </b></summary>

Falha no teste e relata que o teste está desatualizado.

Exemplo de uso:

```c#
  ExceptionsFactory.Depreciated();
```

</details>

<details>
<summary><b> Pending </b></summary>

O teste falha informando que não foi implementado.

Exemplo de uso:

```c#
ExceptionsFactory.Pending();
```

</details>

<details>
<summary><b> FailReportedDefectOrInDevelopmentScenario </b></summary>

Verifica se existe a tag defeito ou bug e falha o teste.

Exemplo de uso:

```c#
ExceptionsFactory.FailReportedDefectOrInDevelopmentScenario(_scenarioContext);
```

</details>

