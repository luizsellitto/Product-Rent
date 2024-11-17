#that archive was made to improve the database organization
use Elegance_Rent;

DELIMITER $$
CREATE PROCEDURE insert_endereco(
    IN p_id INT,
    IN p_cep VARCHAR(10),
    IN p_rua VARCHAR(100),
    IN p_numero INT, 
    IN p_bairro VARCHAR(50),
    IN p_cidade VARCHAR(50),
    IN p_estado VARCHAR(2),
    IN p_tipo_user ENUM('Cliente', 'Funcionário', 'Fornecedor'),
    IN p_id_cli INT,
    IN p_id_fun INT,
    IN p_id_for INT
)
BEGIN
    INSERT INTO Endereco (id, cep, rua, numero, bairro, cidade, estado, tipo_user, id_cli_fk, id_fun_fk, id_for_fk) 
    VALUES (p_id, p_cep, p_rua, p_numero, p_bairro, p_cidade, p_estado, p_tipo_user, p_id_cli, p_id_fun, p_id_for); 
END $$ 
DELIMITER ;

-- Cliente --
DELIMITER $$
CREATE PROCEDURE insert_cliente(
    IN p_nome VARCHAR(100),
    IN p_data_nascimento DATE,
    IN p_sexo VARCHAR(10),
    IN p_rg VARCHAR(12),
    IN p_cnpj VARCHAR(18),
    IN p_cpf VARCHAR(14),
    IN p_telefone VARCHAR(15),
    IN p_email VARCHAR(100),
    IN p_cep VARCHAR(10),
    IN p_rua VARCHAR(100),
    IN p_numero INT,
    IN p_bairro VARCHAR(50),
    IN p_cidade VARCHAR(50),
    IN p_estado VARCHAR(2),
    IN p_tipo_user ENUM('Cliente', 'Funcionário', 'Fornecedor')
)

BEGIN
	DECLARE id INT;
    INSERT INTO Cliente (nome, data_nascimento, sexo, rg, cnpj, cpf, telefone, email, status)
    VALUES (p_nome, p_data_nascimento, p_sexo, p_rg, p_cnpj, p_cpf, p_telefone, p_email, true); 
    IF (LAST_INSERT_ID() IS NULL) THEN
		SET id = 1;
	ELSE
		SET id = LAST_INSERT_ID();
	END IF;
    CALL insert_endereco (NULL, p_cep, p_rua, p_numero, p_bairro, p_cidade, p_estado, p_tipo_user, id, NULL, NULL);
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_cliente()
BEGIN
    SELECT 
	Cliente.id,
	Cliente.nome,
	Cliente.data_nascimento,
	Cliente.sexo,
	Cliente.rg,
    Cliente.cnpj,
	Cliente.cpf, 	
	Cliente.telefone,
	Cliente.email,
	Endereco.cep,
	Endereco.rua,
	Endereco.numero,
	Endereco.bairro,
	Endereco.cidade,
	Endereco.estado,
    Cliente.status
FROM 
	Endereco, Cliente
	WHERE (Endereco.id_cli_fk = Cliente.id) 
	AND (cliente.status = TRUE);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_cliente_id(
	IN id_cli INT
)
BEGIN
    SELECT 
	Cliente.id,
	Cliente.nome,
	Cliente.data_nascimento,
	Cliente.sexo,
	Cliente.rg,
    Cliente.cnpj,
	Cliente.cpf, 	
	Cliente.telefone,
	Cliente.email,
	Endereco.cep,
	Endereco.rua,
	Endereco.numero,
	Endereco.bairro,
	Endereco.cidade,
	Endereco.estado,
    Cliente.status
FROM 
	Endereco, Cliente
	WHERE (Endereco.id_cli_fk = Cliente.id)
	AND Cliente.id = id_cli
    AND (Cliente.status = TRUE);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE update_cliente(
	IN p_id int,
    IN p_nome VARCHAR(100),
    IN p_data_nascimento DATE,
    IN p_sexo VARCHAR(10),
    IN p_rg VARCHAR(12),
    IN p_cnpj VARCHAR(18),
    IN p_cpf VARCHAR(14),
    IN p_telefone VARCHAR(15),
    IN p_email VARCHAR(100),
    IN p_cep VARCHAR(10),
    IN p_rua VARCHAR(100),
    IN p_numero INT,
    IN p_bairro VARCHAR(50),
    IN p_cidade VARCHAR(50),
    IN p_estado VARCHAR(2)
)
BEGIN
    UPDATE Cliente
    SET
        nome = p_nome,
        data_nascimento = p_data_nascimento,
        sexo = p_sexo,
        rg = p_rg,
        cnpj = p_cnpj,
        cpf = p_cpf,
        telefone = p_telefone,
        email = p_email
    WHERE id = p_id;
    #####Endereço#####
    UPDATE Endereco
    SET
        cep = p_cep,
        rua = p_rua,
        numero = p_numero,
        bairro = p_bairro,
        cidade = p_cidade,
        estado = p_estado
    WHERE id_cli_fk = p_id;
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE inative_cliente(
	IN p_id int)
BEGIN
	UPDATE cliente SET status = false where (p_id = id);
END;
$$ DELIMITER ;


#####Funcionário#####
DELIMITER $$
CREATE PROCEDURE insert_funcionario(
    IN p_nome VARCHAR(100),
    IN p_data_nascimento DATE,
    IN p_sexo VARCHAR(10),
    IN p_rg VARCHAR(12),
    IN p_cpf VARCHAR(14), 	
    IN p_telefone VARCHAR(15),
    IN p_email VARCHAR(100),
    IN p_ctps VARCHAR(15),
    IN p_funcao VARCHAR(50),
    IN p_cep VARCHAR(10),
    IN p_rua VARCHAR(100),
    IN p_numero INT,
    IN p_bairro VARCHAR(50),
    IN p_cidade VARCHAR(50),
    IN p_estado VARCHAR(2),
    IN p_tipo_user ENUM('Cliente', 'Funcionário', 'Fornecedor')

)
BEGIN
	DECLARE id INT;
    INSERT INTO Funcionario (nome, data_nascimento, sexo, rg, cpf, telefone, email, ctps, funcao, status)
    VALUES (p_nome, p_data_nascimento, p_sexo, p_rg, p_cpf, p_telefone, p_email, p_ctps, p_funcao, true); 
    IF (LAST_INSERT_ID() IS NULL) THEN
		SET id = 1;
	ELSE
		SET id = LAST_INSERT_ID();
	END IF;
    CALL insert_endereco (NULL, p_cep, p_rua, p_numero, p_bairro, p_cidade, p_estado, p_tipo_user, NULL, id, NULL);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_funcionario()
BEGIN
    SELECT 
	Funcionario.id,
	Funcionario.nome,
	Funcionario.data_nascimento,
	Funcionario.sexo,
	Funcionario.rg,
	Funcionario.cpf, 	
	Funcionario.telefone,
	Funcionario.email,
	Funcionario.ctps,
	Funcionario.funcao,
	Endereco.cep,
	Endereco.rua,
	Endereco.numero,
	Endereco.bairro,
	Endereco.cidade,
	Endereco.estado,
    Funcionario.status
FROM 
	Endereco, Funcionario
	WHERE (Endereco.id_fun_fk = Funcionario.id) 
	AND (funcionario.status = TRUE);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_funcionario_id(
	IN id_fun INT
)
BEGIN
    SELECT 
	Funcionario.id,
	Funcionario.nome,
	Funcionario.data_nascimento,
	Funcionario.sexo,
	Funcionario.rg,
	Funcionario.cpf, 	
	Funcionario.telefone,
	Funcionario.email,
	Funcionario.ctps,
	Funcionario.funcao,
	Endereco.cep,
	Endereco.rua,
	Endereco.numero,
	Endereco.bairro,
	Endereco.cidade,
	Endereco.estado,
    Funcionario.status
FROM 
	Endereco, Funcionario
	WHERE (Endereco.id_fun_fk = Funcionario.id)
	AND Funcionario.id = id_fun
    AND (funcionario.status = TRUE);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE update_funcionario(
    IN p_id INT,
    IN p_nome VARCHAR(100),
    IN p_data_nascimento DATE,
    IN p_sexo VARCHAR(10),
    IN p_rg VARCHAR(12),
    IN p_cpf VARCHAR(14), 	
    IN p_telefone VARCHAR(15),
    IN p_email VARCHAR(100),
    IN p_ctps VARCHAR(15),
    IN p_funcao VARCHAR(50),
    IN p_cep VARCHAR(10),
    IN p_rua VARCHAR(100),
    IN p_numero INT,
    IN p_bairro VARCHAR(50),
    IN p_cidade VARCHAR(50),
    IN p_estado VARCHAR(2)
) 
BEGIN
    UPDATE Funcionario
    SET
        nome = p_nome,
        data_nascimento = p_data_nascimento,
        sexo = p_sexo,
        rg = p_rg,
        cpf = p_cpf,
        telefone = p_telefone,
        email = p_email,
        ctps = p_ctps,
        funcao = p_funcao
    WHERE id = p_id;
    #####Endereço#####
    UPDATE Endereco
    SET
        cep = p_cep,
        rua = p_rua,
        numero = p_numero,
        bairro = p_bairro,
        cidade = p_cidade,
        estado = p_estado
    WHERE id_fun_fk = p_id;
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE inative_funcionario(
	IN p_id int)
BEGIN
	UPDATE funcionario SET status = false where (p_id = id);
END;
$$ DELIMITER ;

#####Fornecedor#####
DELIMITER $$ 
CREATE PROCEDURE insert_fornecedor(
    IN p_razao_social VARCHAR(100),
    IN p_nome_fantasia VARCHAR(100),
    IN p_cnpj VARCHAR(18),
    IN p_inscricao_estadual VARCHAR(20),
    IN p_inscricao_municipal VARCHAR(20),
    IN p_responsavel VARCHAR(100),
    IN p_contato_1 VARCHAR(15),
    IN p_contato_2 VARCHAR(15),
    IN p_contato_3 VARCHAR(15),
    IN p_email_1 VARCHAR(100),
    IN p_email_2 VARCHAR(100),
    IN p_cep VARCHAR(10),
    IN p_rua VARCHAR(100),
    IN p_numero INT,
    IN p_bairro VARCHAR(50),
    IN p_cidade VARCHAR(50),
    IN p_estado VARCHAR(2),
    IN p_tipo_user ENUM('Cliente', 'Funcionário', 'Fornecedor')
)
BEGIN 
	DECLARE id INT;
	
    INSERT INTO Fornecedor (razao_social, nome_fantasia, cnpj, inscricao_estadual, inscricao_municipal, responsavel, contato_1, contato_2, contato_3, email_1, email_2, status)
    VALUES (p_razao_social, p_nome_fantasia, p_cnpj, p_inscricao_estadual, p_inscricao_municipal, p_responsavel, p_contato_1, p_contato_2, p_contato_3, p_email_1, p_email_2, true); 
    IF (LAST_INSERT_ID() IS NULL) THEN
		SET id = 1;
	ELSE
		SET id = LAST_INSERT_ID();
     END IF;
    CALL insert_endereco (NULL, p_cep, p_rua, p_numero, p_bairro, p_cidade, p_estado, p_tipo_user, NULL, NULL, id);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_fornecedor()
BEGIN
    SELECT 
	Fornecedor.id,
	Fornecedor.razao_social,
    Fornecedor.nome_fantasia,
    Fornecedor.cnpj,
    Fornecedor.inscricao_estadual,
    Fornecedor.inscricao_municipal,
    Fornecedor.responsavel,
    Fornecedor.contato_1,
    Fornecedor.contato_2,
    Fornecedor.contato_3,
    Fornecedor.email_1, 
    Fornecedor.email_2,
	Endereco.cep,
	Endereco.rua,
	Endereco.numero,
	Endereco.bairro,
	Endereco.cidade,
	Endereco.estado,
    Fornecedor.status
FROM 
	Endereco
	INNER JOIN Fornecedor ON (Endereco.id_for_fk = Fornecedor.id) 
	WHERE (Fornecedor.status = TRUE);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_fornecedor_id(
	IN id_for INT
)
BEGIN
    SELECT 
	Fornecedor.id,
	Fornecedor.razao_social,
    Fornecedor.nome_fantasia,
    Fornecedor.cnpj,
    Fornecedor.inscricao_estadual,
    Fornecedor.inscricao_municipal,
    Fornecedor.responsavel,
    Fornecedor.contato_1,
    Fornecedor.contato_2,
    Fornecedor.contato_3,
    Fornecedor.email_1, 
    Fornecedor.email_2,
	Endereco.cep,
	Endereco.rua,
	Endereco.numero,
	Endereco.bairro,
	Endereco.cidade,
	Endereco.estado,
    Fornecedor.status
FROM 
	Endereco
	INNER JOIN Fornecedor ON (Endereco.id_for_fk = Fornecedor.id) 
	WHERE Fornecedor.id = id_for
    AND (Fornecedor.status = TRUE);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE update_fornecedor(
    IN p_id INT,
	IN p_razao_social VARCHAR(100),
    IN p_nome_fantasia VARCHAR(100),
    IN p_cnpj VARCHAR(18),
    IN p_inscricao_estadual VARCHAR(20),
    IN p_inscricao_municipal VARCHAR(20),
    IN p_responsavel VARCHAR(100),
    IN p_contato_1 VARCHAR(15),
    IN p_contato_2 VARCHAR(15),
    IN p_contato_3 VARCHAR(15),
    IN p_email_1 VARCHAR(100),
    IN p_email_2 VARCHAR(100),
    IN p_cep VARCHAR(10),
    IN p_rua VARCHAR(100),
    IN p_numero INT,
    IN p_bairro VARCHAR(50),
    IN p_cidade VARCHAR(50),
    IN p_estado VARCHAR(2)
) 
BEGIN
    UPDATE Fornecedor
    SET
        razao_social = p_razao_social,
        nome_fantasia = p_nome_fantasia,
        cnpj = p_cnpj,
        inscricao_estadual = p_inscricao_estadual,
        inscricao_municipal = p_inscricao_municipal,
        responsavel = p_responsavel,
        contato_1 = p_contato_1,
        contato_2 = p_contato_2,
        contato_3 = p_contato_3,
        email_1 = p_email_1,
        email_2 = p_email_2
    WHERE id = p_id;
    #####Endereco#####
    UPDATE Endereco
    SET
        cep = p_cep,
        rua = p_rua,
        numero = p_numero,
        bairro = p_bairro,
        cidade = p_cidade,
        estado = p_estado
    WHERE id_for_fk = p_id;
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE inative_fornecedor(
	IN p_id INT
)
BEGIN
	UPDATE Fornecedor SET status = FALSE WHERE (id = p_id);
END $$
DELIMITER ;


-- Caixa --
DELIMITER $$
CREATE PROCEDURE open_caixa(
    IN p_numero INT,
    IN p_data DATE,
    IN p_saldo_inicial DECIMAL(10, 2),
    IN p_status ENUM('Aberto', 'Fechado'),
    IN p_id_fun INT
)
BEGIN
    INSERT INTO Caixa (numero, data, saldo_inicial, saldo_final, total_recebimentos, total_retiradas, status, id_fun_fk)
    VALUES (p_numero, p_data, p_saldo_inicial, 0, 0, 0, p_status, p_id_fun);
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE close_caixa(
    IN p_id INT,
    IN p_saldo_final DECIMAL(10, 2),
    IN p_total_recebimentos DECIMAL(10, 2),
    IN p_total_retiradas DECIMAL(10, 2),
    IN p_status ENUM('Aberto', 'Fechado')
)
BEGIN
    UPDATE Caixa
    SET saldo_final = p_saldo_final,
        total_recebimentos = p_total_recebimentos,
        total_retiradas = p_total_retiradas,
        status = p_status
    WHERE (id = p_id) AND (status = 'Aberto');
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_caixa_id(
	IN p_id INT
)
BEGIN
    SELECT * FROM Caixa WHERE (id = p_id);
END $$
DELIMITER ;


-- PRODUTO --
DELIMITER $$
CREATE PROCEDURE insert_produto(
    IN p_nome VARCHAR(100),
    IN p_marca VARCHAR(100),
    IN p_tamanho VARCHAR(10),
    IN p_cor VARCHAR(50),
    IN p_valor_aluguel DECIMAL(10, 2), 	
    IN p_descricao VARCHAR(500),
    IN p_id_for INT
)
BEGIN
    INSERT INTO Produto (nome, marca, tamanho, cor, valor_aluguel, descricao, id_for_fk)
    VALUES (p_nome, p_marca, p_tamanho, p_cor, p_valor_aluguel, p_descricao, p_id_for); 
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_produto()
BEGIN
    SELECT 
	produto.id,
	produto.nome,
	produto.marca,
	produto.tamanho,
	produto.cor,
	produto.valor_aluguel, 	
	produto.descricao,
    fornecedor.id,
	fornecedor.nome_fantasia
FROM 
	produto INNER JOIN fornecedor on (produto.id_for_fk = fornecedor.id);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_produto_id(IN id_pro INT)
BEGIN
    SELECT 
	produto.id,
	produto.nome,
	produto.marca,
	produto.tamanho,
	produto.cor,
	produto.valor_aluguel, 	
	produto.descricao,
    fornecedor.id,
	fornecedor.nome_fantasia
FROM 
	produto INNER JOIN fornecedor on (produto.id_for_fk = fornecedor.id)
    AND (produto.id = id_pro);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE update_produto(
    IN p_id INT,
    IN p_nome VARCHAR(100),
    IN p_marca VARCHAR(100),
    IN p_tamanho VARCHAR(10),
    IN p_cor VARCHAR(50),
    IN p_valor_aluguel DECIMAL(10, 2), 	
    IN p_descricao VARCHAR(500),
    IN p_id_for INT
) 
BEGIN
    UPDATE produto
    SET
        nome = p_nome,
        marca = p_marca,
        tamanho = p_tamanho,
        cor = p_cor,
        valor_aluguel = p_valor_aluguel,
        descricao = p_descricao
    WHERE id = p_id;
   
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE inative_produto(
	IN p_id INT
)
BEGIN
	UPDATE produto SET status = FALSE WHERE (id = p_id);
END $$
DELIMITER ;


-- ALUGUEL --
DELIMITER $$
CREATE PROCEDURE insert_aluguel(
    IN p_data_retirada DATE,
    IN p_data_devolucao DATE,
    IN p_valor_total DOUBLE,
    IN p_id_fun_fk INT,
    IN p_id_cli_fk INT
)
BEGIN
    INSERT INTO Aluguel (data_retirada, data_devolucao, valor_total, id_fun_fk, id_cli_fk, status)
    VALUES (p_data_retirada, p_data_devolucao, p_valor_total, p_id_fun_fk, p_id_cli_fk, true); 
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_aluguel()
BEGIN
    SELECT
    aluguel.id,
	aluguel.data_retirada,
	aluguel.data_devolucao,
	aluguel.valor_total,
	aluguel.id_cli_fk,
	aluguel.id_fun_fk,
    aluguel.status
	FROM aluguel WHERE (aluguel.status = true);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_aluguel_id(IN id_alu INT)
BEGIN
    SELECT 
	aluguel.id,
	aluguel.data_retirada,
	aluguel.data_devolucao,
	aluguel.valor_total,
	aluguel.id_cli_fk,
	aluguel.id_fun_fk
FROM 
	aluguel WHERE (id_alu = aluguel.id) and (aluguel.status = true);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE update_aluguel(
	IN p_id INT,
    IN p_data_retirada DATE,
    IN p_data_devolucao DATE,
    IN p_valor_total DOUBLE
) 
BEGIN
    UPDATE aluguel
    SET
        data_retirada = p_data_retirada,
        data_devolucao = p_data_devolucao,
        valor_total = p_valor_total
    WHERE id = p_id;
   
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE inative_aluguel(
	IN p_id int)
BEGIN
	UPDATE aluguel SET status = false where (p_id = id);
END;
$$ DELIMITER ;


-- COMPRA --
DELIMITER $$
CREATE PROCEDURE insert_compra(
    IN p_data DATE,
    IN p_valor_total DOUBLE,
    IN p_forma_de_pagamento VARCHAR(50),
    IN p_id_fun INT,
    IN p_id_for INT
)
BEGIN
    INSERT INTO Compra (data, valor_total, forma_de_pagamento, id_fun_fk, id_for_fk, status)
    VALUES (p_data, p_valor_total, p_forma_de_pagamento, p_id_fun, p_id_for, TRUE); 
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_compra()
BEGIN
    SELECT 
    compra.id,
    compra.data,
    compra.valor_total,
    compra.forma_de_pagamento,
    compra.id_for_fk,
    compra.id_fun_fk
	FROM compra
	WHERE (compra.status = TRUE);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_compra_id(IN id_com INT)
BEGIN
    SELECT 
	compra.id,
    compra.data,
    compra.valor_total,
    compra.forma_de_pagamento,
    compra.id_for_fk,
    compra.id_fun_fk
FROM 
	 compra
     WHERE ((id_com = compra.id) AND (compra.status = TRUE));
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE update_compra(
    IN p_id INT,
    IN p_valor_total DOUBLE,
    IN p_forma_de_pagamento VARCHAR(50)
)
BEGIN
    UPDATE Compra
    SET
        valor_total = p_valor_total,
        forma_de_pagamento = p_forma_de_pagamento
    WHERE id = p_id 
    AND (compra.status = TRUE);
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE inative_compra(
	IN p_id INT
)
BEGIN
	UPDATE compra SET status = FALSE WHERE (id = p_id);
    UPDATE compra SET valor_total = 0 WHERE (id = p_id);
END $$
DELIMITER ;


-- Despesa --
DELIMITER $$
CREATE PROCEDURE insert_despesa(
    IN p_nome VARCHAR(100),
    IN p_data DATETIME,
    IN p_vencimento DATE,
    IN p_parcelamento INT,
    IN p_descricao VARCHAR(300)
)
BEGIN
    INSERT INTO Despesa (nome, data, vencimento, parcelamento, descricao, status)
    VALUES (p_nome, p_data, p_vencimento, p_parcelamento, p_descricao, true);
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_despesa()
BEGIN
    SELECT 
	Despesa.id,
	Despesa.nome,
	Despesa.data,
	Despesa.vencimento,
	Despesa.parcelamento,
    Despesa.descricao,
	Despesa.status
FROM 
	Despesa
	WHERE (Despesa.status = TRUE);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_despesa_id(
	IN id_des INT
)
BEGIN
    SELECT 
	Despesa.id,
	Despesa.nome,
	Despesa.data,
	Despesa.vencimento,
	Despesa.parcelamento,
    Despesa.descricao,
	Despesa.status
FROM
	Despesa
	WHERE (Despesa.status = TRUE) and (Despesa.id = id_des);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE update_despesa(
	IN p_id int,
	IN p_nome VARCHAR(100),
    IN p_data DATETIME,
    IN p_vencimento DATE,
    IN p_parcelamento INT,
    IN p_descricao VARCHAR(300)
)
BEGIN
    UPDATE Despesa
    SET
        nome = p_nome,
        data = p_data,
        vencimento = p_vencimento,
        parcelamento = p_parcelamento,
        descricao = p_descricao
    WHERE id = p_id;
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE inative_despesa(
	IN p_id int)
BEGIN
	UPDATE Despesa SET status = false where (p_id = id);
END;
$$ DELIMITER ;


-- Recebimento --
DELIMITER $$
CREATE PROCEDURE insert_recebimento(
    IN p_status VARCHAR(20),
    IN p_valor DOUBLE,
    IN p_parcela INT,
    IN p_data DATE,
    IN p_forma VARCHAR(50),
    IN p_vencimento DATE,
    IN p_id_cai_fk INT,
    IN p_id_alu_fk INT
)

BEGIN
    INSERT INTO Recebimento (status, valor, parcela, data, forma, vencimento, id_cai_fk, id_alu_fk)
    VALUES (p_status, p_valor, p_parcela, p_data, p_forma, p_vencimento, p_id_cai_fk, p_id_alu_fk);
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_recebimento()
BEGIN
    SELECT
    Recebimento.id,
    Recebimento.status,
    Recebimento.valor,
    Recebimento.parcela,
    Recebimento.data,
    Recebimento.forma,
    Recebimento.vencimento,
    Recebimento.id_cai_fk,
    Recebimento.id_alu_fk
FROM 
	Recebimento;
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_recebimento_id(
	IN id_rec INT
)
BEGIN
    SELECT 
	Recebimento.id,
    Recebimento.status,
    Recebimento.valor,
    Recebimento.parcela,
    Recebimento.data,
    Recebimento.forma,
    Recebimento.vencimento,
    Recebimento.id_cai_fk,
    Recebimento.id_alu_fk
FROM
	Recebimento
	WHERE (Recebimento.id = id_rec);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE update_recebimento(
	IN p_id INT,
	IN p_status VARCHAR(20),
    IN p_valor DOUBLE,
    IN p_parcela INT,
    IN p_data DATE,
    IN p_forma VARCHAR(50),
    IN p_vencimento DATE,
    IN p_id_cai_fk INT,
    IN p_id_alu_fk INT
)
BEGIN
    UPDATE Recebimento
    SET
        status = p_status,
        valor = p_valor,
        parcela = p_parcela,
        data = p_data,
        forma = p_forma,
        vencimento = p_vencimento,
        id_cai_fk = p_id_cai_fk,
        id_alu_fk = p_id_alu_fk
    WHERE id = p_id;
END $$
DELIMITER ;


-- PAGAMENTO --
DELIMITER $$ 
CREATE PROCEDURE insert_pagamento(
	IN p_status BOOL,
    IN p_valor DOUBLE,
    IN p_parcela INT,
    IN p_data DATE,
    IN p_forma VARCHAR(50),
    IN p_id_cai_fk INT,
    IN p_id_des_fk INT,
    IN p_id_comp_fk INT
)
BEGIN
	INSERT INTO Pagamento (status, valor, parcela, data, forma, id_cai_fk, id_des_fk, id_comp_fk)
    VALUES (p_status, p_valor,p_parcela, p_data, p_forma, p_id_cai_fk, p_id_des_fk, p_id_comp_fk);
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_pagamento()
BEGIN
    SELECT
    Pagamento.id,
    Pagamento.status,
    Pagamento.valor,
    Pagamento.parcela,
    Pagamento.data,
    Pagamento.forma,
    Pagamento.id_cai_fk,
    Pagamento.id_des_fk,
    Pagamento.id_comp_fk    
FROM 
	Pagamento;
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE select_pagamento_id(
	IN id_pag INT
)
BEGIN
	SELECT
    Pagamento.id,
    Pagamento.status,
    Pagamento.valor,
    Pagamento.parcela,
    Pagamento.data,
    Pagamento.forma,
    Pagamento.id_cai_fk,
    Pagamento.id_des_fk,
    Pagamento.id_comp_fk    
FROM 
	Pagamento
	WHERE (Pagamento.id = id_pag);
END $$ 
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE update_pagamento(
	IN p_id INT,
    IN p_status BOOL,
    IN p_valor DOUBLE,
    IN p_parcela INT,
    IN p_data DATE,
    IN p_forma VARCHAR(50),
    IN p_id_cai_fk INT,
    IN p_id_des_fk INT,
    IN p_id_comp_fk INT
)
BEGIN
    UPDATE Pagamento
    SET
        status = p_status,
        valor = p_valor,
        parcela = p_parcela,
        data = p_data,
        forma = p_forma,
        id_cai_fk = p_id_cai_fk,
        id_des_fk = p_id_des_fk,
        id_comp_fk = p_id_comp_fk
    WHERE id = p_id;
END $$
DELIMITER ;


## INSERTS PRONTOS
CALL insert_cliente('Ana Souza', '1990-05-12', 'Feminino', '123456789', NULL, '98765432100', '99999-9999', 'ana@gmail.com', '12345-678', 'Rua das Flores', 101, 'Centro', 'São Paulo', 'SP', 'Cliente');
CALL insert_cliente('Carlos Oliveira', '1985-03-25', 'Masculino', '987654321', NULL, '12345678900', '98888-8888', 'carlos@hotmail.com', '65432-111', 'Rua das Rosas', 222, 'Zona Sul', 'Porto Alegre', 'RS', 'Cliente');
CALL insert_cliente('Maria Silva', '1992-08-19', 'Feminino', '567890123', NULL, '45678912300', '97777-7777', 'maria@gmail.com', '11111-222', 'Alameda dos Pássaros', 123, 'Centro', 'Salvador', 'BA', 'Cliente');

CALL insert_funcionario('João Pereira', '1980-01-15', 'Masculino', '321654987', '65498732100', '99999-9999', 'joao@empresa.com', '123456', 'Gerente', '55555-666', 'Rua Alegre', 45, 'Vila Nova', 'Campinas', 'SP', 'Funcionário');
CALL insert_funcionario('Fernanda Lima', '1995-06-30', 'Feminino', '654321987', '98765432100', '98888-8888', 'fernanda@empresa.com', '987654', 'Assistente', '44444-333', 'Avenida Central', 78, 'Centro', 'Belo Horizonte', 'MG', 'Funcionário');
CALL insert_funcionario('Lucas Martins', '1992-11-20', 'Masculino', '789456123', '12378945600', '97777-7777', 'lucas@empresa.com', '111111', 'Analista', '33333-444', 'Rua da Paz', 120, 'Zona Norte', 'Recife', 'PE', 'Funcionário');

CALL insert_fornecedor('Roupas & Cia LTDA', 'Roupas Cia', '12345678000101', '1234567890', '9876543210', 'Pedro Almeida', '99999-9999', '98888-8888', NULL, 'pedro@roupas.com', 'contato@roupas.com', '12345-678', 'Rua das Indústrias', 89, 'Distrito Industrial', 'Fortaleza', 'CE', 'Fornecedor');
CALL insert_fornecedor('Estilo Moda SA', 'Estilo Moda', '98765432000101', '1122334455', '9988776655', 'Roberta Mendes', '97777-7777', NULL, NULL, 'roberta@estilomoda.com', NULL, '65432-111', 'Avenida Fashion', 50, 'Centro', 'Florianópolis', 'SC', 'Fornecedor');
CALL insert_fornecedor('Tecidos e Cores ME', 'Tecidos e Cores', '45678912000101', '2233445566', '1122334455', 'André Santos', '96666-6666', '95555-5555', NULL, 'andre@tecidos.com', 'suporte@tecidos.com', '11111-222', 'Rua da Costura', 123, 'Vila Nova', 'Goiânia', 'GO', 'Fornecedor');

CALL insert_produto('Vestido de Gala', 'Elegance', 'M', 'Vermelho', 250.00, 'Vestido longo ideal para eventos formais.', 1);
CALL insert_produto('Terno Slim', 'Alfa Moda', 'G', 'Preto', 400.00, 'Terno slim fit com corte moderno.', 2);
CALL insert_produto('Gravata Borboleta', 'Acessórios Lux', 'Único', 'Azul', 50.00, 'Gravata borboleta clássica.', 3);
