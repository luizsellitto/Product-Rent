create database Elegance_Rent;
use Elegance_Rent;

create table Endereco (
id_end int primary key not null auto_increment,
cep_end varchar(200),
cidade_end varchar (200),
estado_end varchar (200),
rua_end varchar (200),
bairro_end varchar (200),
numero_end int
);

create table Cliente (
id_cli int primary key not null auto_increment,
nome_cli varchar (1000),
data_nascimento_cli date,
sexo_cli varchar (100),
rg_cli varchar (18),
cnpj_cli varchar (40),
cpf_cli varchar (40),
telefone_cli varchar(16),
email_cli varchar (300),

id_end_fk int,
foreign key (id_end_fk) references Endereco (id_end)
);

create table Funcionario (
id_fun int primary key not null auto_increment,
nome_fun varchar (1000),
cpf_fun varchar (40),
rg_fun varchar (18),
telefone_fun varchar(16),
email_fun varchar (300),
data_nascimento_fun date,
sexo_fun varchar (100),
ctps_fun varchar (150),
funcao_fun varchar (200),

id_end_fk int,
foreign key (id_end_fk) references Endereco (id_end)
);

create table Aluguel (
id_alu int primary key not null auto_increment,
data_retirada_alu date,
data_devolucao_alu date,
valor_total_alu double,

id_fun_fk int,
id_cli_fk int,
foreign key (id_fun_fk) references Funcionario (id_fun),
foreign key (id_cli_fk) references Cliente (id_cli)
);

create table Fornecedor (
id_for int primary key not null auto_increment,
cnpj_for varchar (200),
razao_social_for varchar (200),
nome_fantasia_for varchar (200),
inscricao_estadual_for varchar (200),
inscricao_municipal_for varchar (200),
responsavel_for varchar (200),
contato_1_for varchar (200),
contato_2_for varchar (200),
contato_3_for varchar (200),
email_1_for varchar (200),
email_2_for varchar (200),

id_end_fk int,
foreign key (id_end_fk) references Endereco (id_end)
);

create table Produto (
id_prod int primary key not null auto_increment,
nome_prod varchar (300),
marca_prod varchar (300),
tamanho_prod varchar (300),
cor_prod varchar (300),
valor_aluguel_prod varchar (300),
descricao_prod varchar (500),

id_for_fk int,
foreign key (id_for_fk) references Fornecedor (id_for)
);

create table Compra (
id_comp int primary key not null auto_increment,
valor_total_comp double,
data_comp date,
forma_de_pagamento varchar (300),


id_fun_fk int,
id_for_fk int,
foreign key (id_fun_fk) references Funcionario (id_fun),
foreign key (id_for_fk) references Fornecedor (id_for)
);

create table Compra_Produto (
id_cprod int primary key not null auto_increment,
valor_total_cprod double,
valor_unitario_cprod double,
quantidade_cprod int,

id_prod_fk int,
id_comp_fk int,
foreign key (id_prod_fk) references Produto (id_prod),
foreign key (id_comp_fk) references Compra (id_comp)
);

create table Aluguel_Produto (
id_aprod int primary key not null auto_increment,
valor_total_aprod double,
valor_unitario_aprod double,
quantidade_aprod int,

id_prod_fk int,
id_alu_fk int,
foreign key (id_prod_fk) references Produto (id_prod),
foreign key (id_alu_fk) references Aluguel (id_alu)
);

create table Caixa (
id_cai int primary key not null auto_increment,
numero_cai int,
data_cai date,
saldo_inicial_cai float,
saldo_final_cai float,
total_recebimentos_cai float,
total_retiradas_cai float,

id_fun_fk int,
foreign key (id_fun_fk) references Funcionario (id_fun)
);

create table Recebimento (
id_rec int primary key not null auto_increment,
status_rec varchar (100),
valor_rec double,
parcela_rec int,
data_rec date,
forma_rec varchar (100),
vencimento_rec date,

id_cai_fk int,
id_alu_fk int,
foreign key (id_cai_fk) references Caixa (id_cai),
foreign key (id_alu_fk) references Aluguel (id_alu)
);

create table Despesa (
id_des int primary key not null auto_increment,
nome_des varchar (300),
data_des datetime,
vencimento_des date,
parcelamento_des int,
descricao_des varchar (300)
);

create table Pagamento (
id_pag int primary key not null auto_increment,
status_pag bool,
valor_pag double,
parcela_pag int,
data_pag date,
forma_pag varchar (100),

id_cai_fk int,
id_des_fk int,
id_comp_fk int,
foreign key (id_cai_fk) references Caixa (id_cai),
foreign key (id_des_fk) references Despesa (id_des),
foreign key (id_comp_fk) references Compra (id_comp)
);