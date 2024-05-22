# Descricao
 sistema de controle financeiro pessoal tem como objetivo fornecer uma solução abrangente para indivíduos gerenciarem suas finanças pessoais de forma eficiente e organizada. 

# Passo-a-Passo Build do projeto
 Para compilar a aplicacao, faz-se necessario o uso de banco de dados SQL Server e, criacao das tabelas pertinentes ao negócio, conforme abaixo:

 1 - Baixe a imagem do sql server mcr.microsoft.com/mssql/server e crie um container a partir dela

 2 - Após executar a imagem, conecte-se ao banco a partir do azure data studio e, execute os scripts abaixo:

 - # Tabela de Usuários
 - 
 CREATE TABLE [dbo].[User] (
    Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(256),
    ClientId VARCHAR(256),
    Secret VARCHAR(256),
    CreatedAt DATETIME 
);

 - # Tabela de Despesas

  # =============================================

CREATE TABLE [dbo].[FinancialExpense]
(
    Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    UserId int NOT NULL,
    Category VARCHAR(256),
    Description VARCHAR(500),
    Value int,
    TransactionDate DATETIME,
    CreatedAt DATETIME
);

ALTER TABLE [FinancialExpense] 
ADD CONSTRAINT FK_UserId FOREIGN KEY (UserId)
REFERENCES [User] (Id);

# =============================================
Tabela de Metas

CREATE TABLE [dbo].[FinancialGoal]
(
    Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    UserId int NOT NULL,
    Category VARCHAR(256),
    ActualValue int,
    TargetValue int,
    TargetDate DATETIME,
    Description VARCHAR(500),
    CreatedAt DATETIME
);

ALTER TABLE [FinancialGoal]
ADD CONSTRAINT FK_UserId1 FOREIGN KEY (UserId)
REFERENCES [User] (Id);
