-----------------------------------------------------
-- LIMPEZA GERAL (DROP) - Executar antes de recriar
-----------------------------------------------------

-- Drop das tabelas (na ordem certa por causa das FKs)
BEGIN
  EXECUTE IMMEDIATE 'DROP TABLE TB_COMPRA CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
  EXECUTE IMMEDIATE 'DROP TABLE TB_VINHO CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
  EXECUTE IMMEDIATE 'DROP TABLE TB_USUARIO CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

-- Drop das sequences
BEGIN
  EXECUTE IMMEDIATE 'DROP SEQUENCE usuario_seq';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
  EXECUTE IMMEDIATE 'DROP SEQUENCE vinho_seq';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
  EXECUTE IMMEDIATE 'DROP SEQUENCE compra_seq';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

-- Drop das triggers
BEGIN
  EXECUTE IMMEDIATE 'DROP TRIGGER trg_usuario_pk';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
  EXECUTE IMMEDIATE 'DROP TRIGGER trg_vinho_pk';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
  EXECUTE IMMEDIATE 'DROP TRIGGER trg_compra_pk';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

-----------------------------------------------------
-- RECRIAÇÃO DAS SEQUENCES, TABELAS E TRIGGERS
-----------------------------------------------------

-- Sequências
CREATE SEQUENCE usuario_seq START WITH 1 INCREMENT BY 1 NOCACHE NOCYCLE;
CREATE SEQUENCE vinho_seq START WITH 1 INCREMENT BY 1 NOCACHE NOCYCLE;
CREATE SEQUENCE compra_seq START WITH 1 INCREMENT BY 1 NOCACHE NOCYCLE;

-- Tabela TB_USUARIO
CREATE TABLE TB_USUARIO (
  ID_USUARIO   NUMBER PRIMARY KEY,
  NOME         VARCHAR2(200),
  EMAIL        VARCHAR2(200),
  SENHA        VARCHAR2(100)
);

-- Tabela TB_VINHO
CREATE TABLE TB_VINHO (
  ID_VINHO                    NUMBER PRIMARY KEY,
  NOME                        VARCHAR2(200),
  MARCA                       VARCHAR2(100),
  PAIS_ORIGEM                 VARCHAR2(100),
  TIPO_UVA                    VARCHAR2(100),
  TEMPO_ENVELHECIMENTO        VARCHAR2(100),
  TIPO                        VARCHAR2(100),
  DESCRICAO                   CLOB,
  PERFIL_SABOR                VARCHAR2(200),
  OCASIAO                     VARCHAR2(200),
  HARMONIZACAO                VARCHAR2(200),
  PRECO                       NUMBER(12,2),
  URL_IMAGEM                  VARCHAR2(1000)
);

-- Tabela TB_COMPRA
CREATE TABLE TB_COMPRA (
  ID_COMPRA   NUMBER PRIMARY KEY,
  DATA        DATE,
  QUANTIDADE  NUMBER,
  TOTAL       NUMBER(12,2),
  ID_USUARIO  NUMBER,
  ID_VINHO    NUMBER,
  CONSTRAINT FK_COMPRA_USUARIO FOREIGN KEY (ID_USUARIO) REFERENCES TB_USUARIO(ID_USUARIO),
  CONSTRAINT FK_COMPRA_VINHO FOREIGN KEY (ID_VINHO) REFERENCES TB_VINHO(ID_VINHO)
);

-----------------------------------------------------
-- TRIGGERS PARA GERAR PK AUTOMATICAMENTE
-----------------------------------------------------

CREATE OR REPLACE TRIGGER trg_usuario_pk
BEFORE INSERT ON TB_USUARIO
FOR EACH ROW
BEGIN
  :NEW.ID_USUARIO := usuario_seq.NEXTVAL;
END;
/

CREATE OR REPLACE TRIGGER trg_vinho_pk
BEFORE INSERT ON TB_VINHO
FOR EACH ROW
BEGIN
  :NEW.ID_VINHO := vinho_seq.NEXTVAL;
END;
/

CREATE OR REPLACE TRIGGER trg_compra_pk
BEFORE INSERT ON TB_COMPRA
FOR EACH ROW
BEGIN
  :NEW.ID_COMPRA := compra_seq.NEXTVAL;
END;
/

-----------------------------------------------------
-- MASSAS DE TESTE
-----------------------------------------------------

-- Usuários
INSERT INTO TB_USUARIO (NOME, EMAIL, SENHA) VALUES ('João Almeida', 'joao.almeida@email.com', 'senha123');
INSERT INTO TB_USUARIO (NOME, EMAIL, SENHA) VALUES ('Mariana Costa', 'mariana.costa@email.com', 'mariana@321');
INSERT INTO TB_USUARIO (NOME, EMAIL, SENHA) VALUES ('Pedro Lima', 'pedro.lima@email.com', 'pedro!2024');
INSERT INTO TB_USUARIO (NOME, EMAIL, SENHA) VALUES ('Ana Souza', 'ana.souza@email.com', 'ana#456');
INSERT INTO TB_USUARIO (NOME, EMAIL, SENHA) VALUES ('Lucas Ferreira', 'lucas.ferreira@email.com', 'lucas789');

-- Vinhos
INSERT INTO TB_VINHO (NOME, MARCA, PAIS_ORIGEM, TIPO_UVA, TEMPO_ENVELHECIMENTO, TIPO, DESCRICAO, PERFIL_SABOR, OCASIAO, HARMONIZACAO, PRECO, URL_IMAGEM)
VALUES ('Cabernet Reserva', 'Miolo', 'Brasil', 'Cabernet Sauvignon', '12 meses', 'Tinto Seco',
        'Vinho encorpado e aromático, com notas de frutas vermelhas e especiarias.',
        'Frutado e encorpado', 'Jantar especial', 'Carnes vermelhas e massas', 89.90, 'https://exemplo.com/vinhos/miolo-cabernet.jpg');

INSERT INTO TB_VINHO (NOME, MARCA, PAIS_ORIGEM, TIPO_UVA, TEMPO_ENVELHECIMENTO, TIPO, DESCRICAO, PERFIL_SABOR, OCASIAO, HARMONIZACAO, PRECO, URL_IMAGEM)
VALUES ('Malbec Premium', 'Trapiche', 'Argentina', 'Malbec', '18 meses', 'Tinto Seco',
        'Aveludado, com aroma intenso e taninos equilibrados.',
        'Encorpado e intenso', 'Reuniões e jantares', 'Churrasco e queijos fortes', 119.50, 'https://exemplo.com/vinhos/trapiche-malbec.jpg');

INSERT INTO TB_VINHO (NOME, MARCA, PAIS_ORIGEM, TIPO_UVA, TEMPO_ENVELHECIMENTO, TIPO, DESCRICAO, PERFIL_SABOR, OCASIAO, HARMONIZACAO, PRECO, URL_IMAGEM)
VALUES ('Chardonnay Clássico', 'Concha y Toro', 'Chile', 'Chardonnay', '6 meses', 'Branco Seco',
        'Refrescante e equilibrado, com notas de frutas tropicais e toques de baunilha.',
        'Suave e refrescante', 'Dias quentes e aperitivos', 'Peixes e saladas', 79.00, 'https://exemplo.com/vinhos/chardonnay.jpg');

INSERT INTO TB_VINHO (NOME, MARCA, PAIS_ORIGEM, TIPO_UVA, TEMPO_ENVELHECIMENTO, TIPO, DESCRICAO, PERFIL_SABOR, OCASIAO, HARMONIZACAO, PRECO, URL_IMAGEM)
VALUES ('Merlot Clássico', 'Salton', 'Brasil', 'Merlot', '8 meses', 'Tinto Suave',
        'Equilibrado e aromático, com taninos leves e notas de frutas maduras.',
        'Suave e frutado', 'Eventos e almoços', 'Massas leves e queijos', 59.90, 'https://exemplo.com/vinhos/salton-merlot.jpg');

INSERT INTO TB_VINHO (NOME, MARCA, PAIS_ORIGEM, TIPO_UVA, TEMPO_ENVELHECIMENTO, TIPO, DESCRICAO, PERFIL_SABOR, OCASIAO, HARMONIZACAO, PRECO, URL_IMAGEM)
VALUES ('Rosé Provence', 'Domaines Ott', 'França', 'Grenache', '4 meses', 'Rosé',
        'Leve e aromático, com toque floral e final fresco.',
        'Delicado e refrescante', 'Verão e piqueniques', 'Frutos do mar e saladas', 139.00, 'https://exemplo.com/vinhos/rose-provence.jpg');

-- Compras simuladas
INSERT INTO TB_COMPRA (DATA, QUANTIDADE, TOTAL, ID_USUARIO, ID_VINHO) VALUES (SYSDATE - 10, 2, 179.80, 1, 1);
INSERT INTO TB_COMPRA (DATA, QUANTIDADE, TOTAL, ID_USUARIO, ID_VINHO) VALUES (SYSDATE - 5, 1, 119.50, 2, 2);
INSERT INTO TB_COMPRA (DATA, QUANTIDADE, TOTAL, ID_USUARIO, ID_VINHO) VALUES (SYSDATE - 3, 3, 177.00, 3, 4);
INSERT INTO TB_COMPRA (DATA, QUANTIDADE, TOTAL, ID_USUARIO, ID_VINHO) VALUES (SYSDATE - 2, 1, 79.00, 4, 3);
INSERT INTO TB_COMPRA (DATA, QUANTIDADE, TOTAL, ID_USUARIO, ID_VINHO) VALUES (SYSDATE - 1, 2, 278.00, 5, 5);

COMMIT;
