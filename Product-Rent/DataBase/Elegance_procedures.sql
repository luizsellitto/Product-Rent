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
    IN p_id_fun int
)
BEGIN
    INSERT INTO Endereco (id, cep, rua, numero, bairro, cidade, estado, tipo_user, id_fun_fk) 
    VALUES (p_id, p_cep, p_rua, p_numero, p_bairro, p_cidade, p_estado, p_tipo_user, p_id_fun); 
END $$ 
DELIMITER ;

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
	declare id int;
	
    INSERT INTO Funcionario (nome, data_nascimento, sexo, rg, cpf, telefone, email, ctps, funcao, ativo)
    VALUES (p_nome, p_data_nascimento, p_sexo, p_rg, p_cpf, p_telefone, p_email, p_ctps, p_funcao, true); 
    if (LAST_INSERT_ID() is null) then
		set id = 1;
	else
		set id = LAST_INSERT_ID();
     end if;
    CALL insert_address (NULL, p_cep, p_rua, p_numero, p_bairro, p_cidade, p_estado, p_tipo_user, id);
END $$ 
DELIMITER ;
-- DROP PROCEDURE insert_funcionario;

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
    #Endereco.id_fun_fk
FROM 
	Endereco, Funcionario
	WHERE (Endereco.id_fun_fk = Funcionario.id) 
	AND (ativo = 1);
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
    AND (ativo = 1);
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
    -- Atualiza os dados do funcionário
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

    -- Atualiza os dados do endereço do funcionário
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
CREATE PROCEDURE delete_funcionario(
	IN p_id int)
BEGIN
	UPDATE funcionario SET ativo = false where (p_id = id);
END;
$$ DELIMITER ;