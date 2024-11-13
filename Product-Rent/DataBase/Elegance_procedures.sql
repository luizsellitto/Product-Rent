#that archive was made to improve the database organization
use Elegance_Rent;

DELIMITER $$
CREATE PROCEDURE insert_address(
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

#####Cliente#####
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
    CALL insert_address (NULL, p_cep, p_rua, p_numero, p_bairro, p_cidade, p_estado, p_tipo_user, id, NULL, NULL);
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
    CALL insert_address (NULL, p_cep, p_rua, p_numero, p_bairro, p_cidade, p_estado, p_tipo_user, NULL, id, NULL);
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
    CALL insert_address (NULL, p_cep, p_rua, p_numero, p_bairro, p_cidade, p_estado, p_tipo_user, NULL, NULL, id);
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

select * from produto;

drop PROCEDURE select_fornecedor;
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

#####Caixa#####
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
    INSERT INTO Aluguel (data_retirada, data_devolucao, valor_total, id_fun_fk, id_cli_fk)
    VALUES (p_data_retirada, p_data_devolucao, p_valor_total, p_id_fun_fk, p_id_cli_fk); 
END $$ 
DELIMITER ;
drop procedure insert_aluguel;
DELIMITER $$
CREATE PROCEDURE select_aluguel()
BEGIN
    SELECT
    aluguel.id,
	aluguel.data_retirada,
	aluguel.data_devolucao,
	aluguel.valor_total,
	aluguel.id_cli_fk as 'ID cliente',
    cliente.nome as 'Nome cliente',
	aluguel.id_fun_fk as 'ID funcionário',
    funcionario.nome as 'Nome funcionário'
FROM 
	aluguel INNER JOIN funcionario ON (aluguel.id_fun_fk = funcionario.id)
    INNER JOIN cliente ON (aluguel.id_cli_fk = cliente.id);
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
	aluguel.id_cli_fk as 'ID cliente',
    cliente.nome as 'Nome cliente',
	aluguel.id_fun_fk as 'ID funcionário',
    funcionario.nome as 'Nome funcionário'

FROM 
	aluguel INNER JOIN cliente ON (aluguel.id_cli_fk = cliente.id)
    INNER JOIN funcionario ON (aluguel.id_fun_fk = funcionario.id)
    AND (id_alu = aluguel.id);
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
        data_devoluca = p_data_devolucao,
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


/*
CALL insert_fornecedor('Razão Social Exemplo', 'Nome Fantasia Exemplo', '12.345.678/0001-90', '123456789', '987654321', 'Carlos Silva', '(11) 12345-6789', '(11) 98765-4321', '(11) 23456-7890', 'contato1@email.com', 'contato2@email.com', '12345-678', 'Rua Exemplo', 500, 'Centro', 'São Paulo', 'SP', 'Fornecedor');
CALL insert_fornecedor('Razão Social Exemplo', 'Nome Fantasia Exemplo', '12.345.678/0001-90', '123456789', '987654321', 'Carlos Silva', '(11) 12345-6789', '(11) 98765-4321', '(11) 23456-7890', 'contato1@email.com', 'contato2@email.com', '12345-678', 'Rua Exemplo', 500, 'Centro', 'São Paulo', 'SP', 'Fornecedor');
CALL insert_funcionario('João Silva', '1990-05-15', 'Masculino', '123456789', '123.456.789-00', '(11) 98765-4321', 'joao.silva@email.com', '123456789', 'Gerente', '12345-678', 'Rua Exemplo', 100, 'Centro', 'São Paulo', 'SP', 'Funcionário');
CALL insert_funcionario('João Silva', '1990-05-15', 'Masculino', '123456789', '123.456.789-00', '(11) 98765-4321', 'joao.silva@email.com', '123456789', 'Gerente', '12345-678', 'Rua Exemplo', 100, 'Centro', 'São Paulo', 'SP', 'Funcionário');
*/