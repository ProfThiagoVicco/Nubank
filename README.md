# 💰 Capital Gain Calculator - Nubank Code Challenge

#Nome: Thiago Keller

## 📌 Sobre o Projeto

Esta solução implementa um programa de linha de comando (CLI) em C# para calcular imposto sobre ganho de capital com base em operações de compra e venda de ações. A entrada e saída seguem o formato JSON, conforme especificado no desafio.

---

## 🧠 Decisões Técnicas e Arquiteturais

- **Clean Code & DDD (Domain-Driven Design):**
  - As regras de negócio foram encapsuladas dentro de modelos ricos (`BuyOperation`, `SellOperation`, `Portfolio`, `TaxResult`).
  - O domínio é isolado da lógica de entrada/saída, facilitando testes, manutenção e reuso.
  
- **Polimorfismo e Orientação a Objetos:**
  - Cada tipo de operação (`buy`, `sell`) tem sua própria lógica via herança de `TradeOperation`.

- **Separação de Responsabilidades:**
  - `Program.cs` lida exclusivamente com entrada/saída.
  - O cálculo do imposto é responsabilidade do domínio (`Portfolio`).
  - DTOs (`OperationDto`) são utilizados para deserialização JSON e conversão para objetos de domínio.

---

## 📦 Frameworks e Bibliotecas

- `System.Text.Json` — para parsing de JSON de forma leve e sem dependências externas.
- `xUnit` — para testes unitários e de integração.
- `Microsoft.NET.Test.Sdk` — para execução de testes via CLI.

> Não foram utilizados frameworks pesados ou desnecessários, conforme recomendado no desafio.
