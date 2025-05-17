# Mottu Bracelet

## 📌 Descrição do Projeto

O Mottu Bracelet é um projeto desenvolvido para a empresa Mottu, que visa o gerenciamento eficiente de suas motos nos pátios de manutenção. Trata-se de um bracelete que é acoplado às motos que chegam ao pátio.
Esse bracelete é configurado através de um aplicativo integrando todos os dados da moto com os dados do pátio e do próprio dispositivo. Para encontrar a moto no pátio, basta acionar o bracelete através do dispositivo e 
esse emitirá sinal sonoro e sinal infravermelho capaz de ser visualizado através da câmera do aplicativo.

Este repositório contém uma versão preliminar do código backend que se integra tanto com o banco de dados quanto com a aplicação front-end. Trata-se de uma API Restful que utiliza ASP.NET Core.
---

## 👨‍💻 Integrantes

- Pedro Abrantes Andrade | RM558186
- Ricardo Tavares de Oliveira Filho | RM556092
- Victor Alves Carmona | RM555726

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core Web API
- C#
- Entity Framework Core
- Banco de Dados Oracle
- Swagger
- JSON
- Visual Studio

---

## 📂 Instalação e Execução

### Pré-requisitos

- .NET 6.0 ou superior
- Visual Studio 2022 ou superior
- Acesso ao banco de dados Oracle
- Conta com usuário e senha válidos

### Executando o projeto

1. Clone o repositório:

   ```
   git clone https://github.com/seuusuario/mottu-bracelet.git
   ```

2. Abra o projeto no Visual Studio.

3. Verifique se a string de conexão no `appsettings.json` está correta:

  ```
  "ConnectionStrings": {
  "DefaultConnection": "Data Source=oracle.fiap.com.br:1521/orcl; User Id='seu-usuario'; Password='sua-senha';"
}
```

4. Rode a aplicação clicando no botão de execução com o protocolo HTTPS selecionado. O Swagger será iniciado automaticamente com os endpoints disponíveis.

## 📡 Endpoints da API

### 🔧 MotoController

| Método | Endpoint             | Descrição                                        |
|--------|----------------------|--------------------------------------------------|
| GET    | `/api/Moto`          | Retorna todas as motos com seus respectivos pátios e dispositivos. |
| GET    | `/api/Moto/{id}`     | Retorna uma moto específica por ID.             |
| POST   | `/api/Moto`          | Cria uma nova moto e associa ao dispositivo informado. |
| PUT    | `/api/Moto/{id}`     | Atualiza uma moto existente.                    |
| DELETE | `/api/Moto/{id}`     | Remove uma moto do sistema.                     |

---

### 🔧 DispositivoController

| Método | Endpoint                  | Descrição                               |
|--------|---------------------------|-----------------------------------------|
| GET    | `/api/Dispositivo`        | Lista todos os dispositivos.            |
| GET    | `/api/Dispositivo/{id}`   | Retorna um dispositivo específico.      |
| POST   | `/api/Dispositivo`        | Cria um novo dispositivo.               |
| PUT    | `/api/Dispositivo/{id}`   | Atualiza as informações de um dispositivo. |
| DELETE | `/api/Dispositivo/{id}`   | Remove um dispositivo.                  |

---

### 🔧 PatioController

| Método | Endpoint             | Descrição                           |
|--------|----------------------|-------------------------------------|
| GET    | `/api/Patio`         | Retorna todos os pátios cadastrados. |
| GET    | `/api/Patio/{id}`    | Retorna um pátio específico.        |
| POST   | `/api/Patio`         | Cria um novo pátio.                 |
| PUT    | `/api/Patio/{id}`    | Atualiza informações do pátio.      |
| DELETE | `/api/Patio/{id}`    | Remove um pátio do sistema.         |

---

### 🔧 HistoricoPatioController

| Método | Endpoint                    | Descrição                                                |
|--------|-----------------------------|----------------------------------------------------------|
| GET    | `/api/HistoricoPatio`       | Lista todos os registros de histórico.                   |
| GET    | `/api/HistoricoPatio/{id}`  | Retorna um registro de histórico específico.             |
| POST   | `/api/HistoricoPatio`       | Cria um novo registro de movimentação de moto entre pátios. |

Observação: para respeitar o relacionamento entre as tabelas é necessário criar objetos na seguinte ordem:
patio -> dispositivo -> moto -> historicoPatio

