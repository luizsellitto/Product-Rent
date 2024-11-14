create database Elegance_Rent;
use Elegance_Rent;

CREATE TABLE Cliente (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    nome VARCHAR(100),
    data_nascimento DATE,
    sexo VARCHAR(10),
    rg VARCHAR(12),
    cpf VARCHAR(14),
    cnpj VARCHAR(18),
    telefone VARCHAR(15),
    email VARCHAR(100),
    status bool
);

CREATE TABLE Funcionario (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    nome VARCHAR(100),
    data_nascimento DATE,
    sexo VARCHAR(10),
    rg VARCHAR(12),
    cpf VARCHAR(14),
    ctps VARCHAR(15),
    funcao VARCHAR(50),
    telefone VARCHAR(15),
    email VARCHAR(100),
    status bool
);

CREATE TABLE Fornecedor (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    razao_social VARCHAR(100),
    nome_fantasia VARCHAR(100),
    cnpj VARCHAR(18),
    inscricao_estadual VARCHAR(20),
    inscricao_municipal VARCHAR(20),
    responsavel VARCHAR(100),
    contato_1 VARCHAR(15),
    contato_2 VARCHAR(15),
    contato_3 VARCHAR(15),
    email_1 VARCHAR(100),
    email_2 VARCHAR(100),
    status bool
);
# alter table fornecedor add status bool;
CREATE TABLE Endereco (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    cep VARCHAR(10),
    rua VARCHAR(100),
    numero INT,
    bairro VARCHAR(50),
    cidade VARCHAR(50),
    estado VARCHAR(2),
    tipo_user ENUM('Cliente', 'Funcionário', 'Fornecedor'),
    id_cli_fk INT UNIQUE,
    id_fun_fk INT UNIQUE,
    id_for_fk INT UNIQUE,
    FOREIGN KEY (id_cli_fk) REFERENCES Cliente (id),
    FOREIGN KEY (id_fun_fk) REFERENCES Funcionario (id),
    FOREIGN KEY (id_for_fk) REFERENCES Fornecedor (id)
);

#insert into endereco (id, cep, rua, numero, bairro, cidade, estado, tipo_user) values (null, 'cep', 'rua', 520, 'bairro', 'cidade', 'es', 'cliente');

CREATE TABLE Aluguel (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    data_retirada DATE,
    data_devolucao DATE,
    valor_total DOUBLE,
    id_fun_fk INT,
    id_cli_fk INT,
    status bool,
    FOREIGN KEY (id_fun_fk) REFERENCES Funcionario (id),
    FOREIGN KEY (id_cli_fk) REFERENCES Cliente (id)
);

CREATE TABLE Produto (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    nome VARCHAR(100),
    marca VARCHAR(100),
    tamanho VARCHAR(10),
    cor VARCHAR(30),
    valor_aluguel DECIMAL(10, 2),
    descricao VARCHAR(500),
    id_for_fk INT,
    FOREIGN KEY (id_for_fk) REFERENCES Fornecedor (id)
);

CREATE TABLE Compra (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    data DATE,
    valor_total DOUBLE,
    forma_de_pagamento VARCHAR(50),
    id_fun_fk INT,
    id_for_fk INT,
    FOREIGN KEY (id_fun_fk) REFERENCES Funcionario (id),
    FOREIGN KEY (id_for_fk) REFERENCES Fornecedor (id),
    status bool
);
-- FALTA-----------------------
CREATE TABLE Compra_Produto ( 
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    valor_total DOUBLE,
    valor_unitario DOUBLE,
    quantidade INT,
    id_prod_fk INT,
    id_comp_fk INT,
    FOREIGN KEY (id_prod_fk) REFERENCES Produto (id),
    FOREIGN KEY (id_comp_fk) REFERENCES Compra (id)
);
-- FALTA-----------------------
CREATE TABLE Aluguel_Produto (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    valor_total DOUBLE,
    valor_unitario DOUBLE,
    quantidade INT,
    id_prod_fk INT,
    id_alu_fk INT,
    FOREIGN KEY (id_prod_fk) REFERENCES Produto (id),
    FOREIGN KEY (id_alu_fk) REFERENCES Aluguel (id)
);

CREATE TABLE Caixa (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    numero INT,
    data DATE,
    saldo_inicial DECIMAL(10, 2),
    saldo_final DECIMAL(10, 2),
    total_recebimentos DECIMAL(10, 2),
    total_retiradas DECIMAL(10, 2),
    status ENUM('Aberto', 'Fechado'),
    id_fun_fk INT,
    FOREIGN KEY (id_fun_fk) REFERENCES Funcionario (id)
);
-- Recebimento-----------------------
CREATE TABLE Recebimento (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    status VARCHAR(20),
    valor DOUBLE,
    parcela INT,
    data DATE,
    forma VARCHAR(50),
    vencimento DATE,
    id_cai_fk INT,
    id_alu_fk INT,
    FOREIGN KEY (id_cai_fk) REFERENCES Caixa (id),
    FOREIGN KEY (id_alu_fk) REFERENCES Aluguel (id)
);
-- Despesa-------------------
CREATE TABLE Despesa (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    nome VARCHAR(100),
    data DATETIME,
    vencimento DATE,
    parcelamento INT,
    descricao VARCHAR(300),
    status bool
);

-- FALTA-----------------------
CREATE TABLE Pagamento (
    id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    status BOOL,
    valor DOUBLE,
    parcela INT,
    data DATE,
    forma VARCHAR(50),
    id_cai_fk INT,
    id_des_fk INT,
    id_comp_fk INT,
    FOREIGN KEY (id_cai_fk) REFERENCES Caixa (id),
    FOREIGN KEY (id_des_fk) REFERENCES Despesa (id),
    FOREIGN KEY (id_comp_fk) REFERENCES Compra (id)
);
