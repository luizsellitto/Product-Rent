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

-- CLIENTE --
DELIMITER $$
CREATE PROCEDURE insert_client(
    IN p_id INT,
    IN p_nome VARCHAR(100),
    IN p_data_nascimento DATE,
    IN p_sexo VARCHAR(10),
    IN p_rg VARCHAR(12),
    IN p_cnpj VARCHAR(18),
    IN p_cpf VARCHAR(14), 	
    IN p_telefone VARCHAR(15),
    IN p_email VARCHAR(100)
)
BEGIN
    INSERT INTO Cliente
    VALUES (p_id, p_nome, p_data_nascimento, p_sexo, p_rg, p_cnpj, p_cpf, p_telefone, p_email); 
END $$ 
DELIMITER ;

-- FUNCIONÁRIO --
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
    INSERT INTO Funcionario (nome, data_nascimento, sexo, rg, cpf, telefone, email, ctps, funcao, ativo)
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
    Funcionario.ativo
FROM 
	Endereco, Funcionario
	WHERE (Endereco.id_fun_fk = Funcionario.id) 
	AND (funcionario.ativo = TRUE);
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
    Funcionario.ativo
FROM 
	Endereco, Funcionario
	WHERE (Endereco.id_fun_fk = Funcionario.id)
	AND Funcionario.id = id_fun
    AND (funcionario.ativo = TRUE);
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
    -- endereço
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
	UPDATE funcionario SET ativo = false where (p_id = id);
END;
$$ DELIMITER ;

-- FORNECEDOR --
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
	
    INSERT INTO Fornecedor (razao_social, nome_fantasia, cnpj, inscricao_estadual, inscricao_municipal, responsavel, contato_1, contato_2, contato_3, email_1, email_2, ativo)
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
    Fornecedor.ativo
FROM 
	Endereco
	INNER JOIN Fornecedor ON (Endereco.id_for_fk = Fornecedor.id) 
	WHERE (Fornecedor.ativo = TRUE);
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
    Fornecedor.ativo
FROM 
	Endereco
	INNER JOIN Fornecedor ON (Endereco.id_for_fk = Fornecedor.id) 
	WHERE Fornecedor.id = id_for
    AND (Fornecedor.ativo = TRUE);
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
    -- endereço
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
	UPDATE Fornecedor SET ativo = FALSE WHERE (id = p_id);
END $$
DELIMITER ;

-- CAIXA --
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
    WHERE id = p_id AND status = 'Aberto';
END $$
DELIMITER ;
