<div align="center">

<h1>🖨️ AD2Graf</h1>
<p><strong>Sistema Web de Gestão para Gráfica</strong></p>

<p>
  <img src="https://img.shields.io/badge/C%23-512BD4?style=flat&logo=dotnet&logoColor=white"/>
  <img src="https://img.shields.io/badge/ASP.NET_Core_MVC-512BD4?style=flat&logo=dotnet&logoColor=white"/>
  <img src="https://img.shields.io/badge/Entity_Framework_Core-512BD4?style=flat&logo=dotnet&logoColor=white"/>
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=flat&logo=microsoftsqlserver&logoColor=white"/>
  <img src="https://img.shields.io/badge/Bootstrap_5-7952B3?style=flat&logo=bootstrap&logoColor=white"/>
</p>

<p>
  <img src="https://img.shields.io/badge/Status-Concluído-28a745?style=flat"/>
  <img src="https://img.shields.io/badge/Semestre-4º-3d3d3d?style=flat"/>
  <img src="https://img.shields.io/badge/Fatec-Bragança_Paulista-003b6f?style=flat"/>
</p>

</div>

---

## 📌 Sobre o Projeto

O **AD2Graf** é um sistema web de gestão desenvolvido como projeto acadêmico no **4º semestre de GTI na Fatec Bragança Paulista**, com o objetivo de resolver um problema real: uma gráfica sem controle digital de estoque, pedidos e serviços — tudo feito no improviso.

O sistema permite controlar os materiais em estoque, registrar entradas e saídas, gerenciar pedidos de clientes e cadastrar os serviços oferecidos pela gráfica, tudo em uma interface web limpa e responsiva.

---

## 🖥️ Telas do Sistema

<div align="center">

| Home | Controle de Estoque |
|------|-------------------|
| Tela inicial com acesso rápido a todos os módulos | Visão em tempo real dos materiais disponíveis |

| Movimentações | Gestão de Pedidos |
|--------------|------------------|
| Histórico de entradas e saídas com atualização automática | Pedidos com controle de status por etapa |

</div>

---

## ⚙️ Funcionalidades

### 📦 Insumos
- Cadastro de materiais utilizados na produção
- Ao cadastrar um insumo, o estoque é criado automaticamente com saldo zero
- Inativação segura — impede remover insumo com saldo em estoque
- Edição com atualização automática do preço no estoque vinculado

### 📊 Estoque
- Visão em tempo real do saldo de cada material
- Exibe data da última movimentação por insumo
- Atualizado automaticamente a cada entrada ou saída registrada

### ↔️ Movimentações
- Registro de entradas e saídas de materiais
- Validação de saldo negativo — impede saída maior que o disponível
- Exclusão com estorno automático do impacto no estoque
- Histórico ordenado da movimentação mais recente para a mais antiga

### 📋 Pedidos
- Cadastro de pedidos de clientes com empresa, descrição, quantidade e preço
- Cálculo automático do valor total (quantidade × preço unitário)
- Controle de status: **Pendente → Em Produção → Concluído / Cancelado**

### 🛠️ Serviços
- Cadastro dos tipos de serviços oferecidos pela gráfica
- Nome e preço base como referência para formação de preço dos pedidos

---

## 🏗️ Arquitetura

O projeto segue o padrão **MVC (Model-View-Controller)** com camadas adicionais de **Repository** e **Service**, separando responsabilidades de forma clara:

```
AD2Graf/
├── Controllers/        # Recebe requisições e direciona o fluxo
├── Models/             # Entidades do domínio (Insumo, Estoque, Pedido...)
├── Views/              # Interface HTML com Razor
├── Data/               # DbContext — configuração do banco com EF Core
├── Repositorios/       # Acesso ao banco de dados (Repository Pattern)
├── Servicos/           # Regras de negócio (Service Layer)
└── Migrations/         # Histórico de alterações no banco
```

### Injeção de Dependência

```csharp
// Program.cs
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
```

---

## 🛠️ Stack Tecnológica

| Tecnologia | Uso |
|---|---|
| **C# / ASP.NET Core MVC** | Backend e estrutura da aplicação |
| **Entity Framework Core** | ORM para comunicação com o banco |
| **SQL Server (LocalDB)** | Banco de dados relacional |
| **Razor Pages** | Engine de templates para as views |
| **Bootstrap 5** | Estilização e responsividade |
| **Bootstrap Icons** | Ícones da interface |


```

- Cada **Insumo** tem exatamente um registro de **Estoque** vinculado
- Cada **Movimentação** referencia um Insumo e atualiza automaticamente seu Estoque
- **Pedidos** e **Serviços** são entidades independentes

---

## 👥 Autores

Desenvolvido em dupla no 4º semestre de GTI — Fatec Bragança Paulista.

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/guigs-godoy">
        <img src="https://github.com/guigs-godoy.png" width="80" style="border-radius:50%"/><br/>
        <strong>Guilherme Godoy</strong>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/Gui369SI">
        <img src="https://github.com/Gui369SI.png" width="80" style="border-radius:50%"/><br/>
        <strong>Guilherme Monteiro</strong>
      </a>
    </td>
  </tr>
</table>

---

<div align="center">
  <p>Projeto acadêmico — Fatec Bragança Paulista · 2026</p>
</div>
