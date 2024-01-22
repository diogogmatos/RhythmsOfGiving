-- USE master;
-- GO
-- ALTER DATABASE RhythmsOfGiving
-- SET SINGLE_USER
-- WITH ROLLBACK IMMEDIATE;

-- Drop database RhythmsOfGiving;

-- CREATE DATABASE RhythmsOfGiving;

-- USE RhythmsOfGiving;

GO
CREATE TABLE [Administrador] (
    id int NOT NULL UNIQUE,
    palavraPasse varchar(45) NOT NULL,
    email varchar(100) NOT NULL UNIQUE,
    CONSTRAINT [PK_ADMINISTRADOR] PRIMARY KEY CLUSTERED ([id] ASC) WITH (IGNORE_DUP_KEY = OFF)
);
GO
CREATE TABLE [GeneroMusical] (
    id int NOT NULL UNIQUE,
    nome varchar(45) NOT NULL UNIQUE,
    idAdministrador int NOT NULL,
    CONSTRAINT [PK_GENEROMUSICAL] PRIMARY KEY CLUSTERED ([id] ASC) WITH (IGNORE_DUP_KEY = OFF),
    CONSTRAINT [GeneroMusical_fk0] FOREIGN KEY ([idAdministrador]) REFERENCES [Administrador]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO
CREATE TABLE [Artista] (
    id int NOT NULL UNIQUE,
    imagem varchar(200) NOT NULL,
    nome varchar(45) NOT NULL UNIQUE,
    idAdministrador int NOT NULL,
    CONSTRAINT [PK_ARTISTA] PRIMARY KEY CLUSTERED ([id] ASC) WITH (IGNORE_DUP_KEY = OFF),
    CONSTRAINT [Artista_fk0] FOREIGN KEY ([idAdministrador]) REFERENCES [Administrador]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO
CREATE TABLE [Instituicao] (
    id int NOT NULL UNIQUE,
    nome varchar(45) NOT NULL UNIQUE,
    descricao varchar(MAX) NOT NULL,
    logotipo varchar(200) NOT NULL,
    hiperligacao varchar(100) NOT NULL,
    iban varchar(100) NOT NULL UNIQUE,
    idAdministrador int NOT NULL,
    CONSTRAINT [PK_INSTITUICAO] PRIMARY KEY CLUSTERED ([id] ASC) WITH (IGNORE_DUP_KEY = OFF),
    CONSTRAINT [Instituicao_fk0] FOREIGN KEY ([idAdministrador]) REFERENCES [Administrador]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO
CREATE TABLE [Licitador] (
    id int NOT NULL UNIQUE,
    nome varchar(45) NOT NULL,
    palavraPasse varchar(45) NOT NULL,
    dataNascimento date NOT NULL,
    nrCartao int NOT NULL,
    email varchar(100) NOT NULL UNIQUE,
    nif int NOT NULL UNIQUE,
    numeroCC bigint NOT NULL UNIQUE,
    CONSTRAINT [PK_LICITADOR] PRIMARY KEY CLUSTERED ([id] ASC) WITH (IGNORE_DUP_KEY = OFF)
);
GO
CREATE TABLE [Leilao] (
    id INT NOT NULL UNIQUE,
    titulo VARCHAR(100) NOT NULL,
    valorAtual DECIMAL(7,2) NOT NULL,
    valorMinimo DECIMAL(7,2) NOT NULL,
    dataHoraFinal DATETIME NOT NULL,
    localizacao VARCHAR(100) NOT NULL,
    estado BIT NOT NULL,
    tipoLeilao VARCHAR(45) NOT NULL,
    descricao VARCHAR(MAX) NOT NULL,
    imagem VARCHAR(MAX) NOT NULL,
    idArtista INT NOT NULL,
    idGeneroMusical INT NOT NULL,
    idAdministrador INT NOT NULL,
    idInstituicao INT NULL,
    CONSTRAINT [PK_LEILAO] PRIMARY KEY CLUSTERED ([id] ASC) WITH (IGNORE_DUP_KEY = OFF),
    CONSTRAINT [Leilao_fk0] FOREIGN KEY ([idArtista]) REFERENCES [Artista]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION,
    CONSTRAINT [Leilao_fk1] FOREIGN KEY ([idGeneroMusical]) REFERENCES [GeneroMusical]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION,
    CONSTRAINT [Leilao_fk2] FOREIGN KEY ([idAdministrador]) REFERENCES [Administrador]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION,
	CONSTRAINT [Leilao_fk3] FOREIGN KEY ([idInstituicao]) REFERENCES [Instituicao]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO
CREATE TABLE [Licitacao] (
    id int NOT NULL UNIQUE,
    dataHora datetime NOT NULL,
    valor decimal(7,2) NOT NULL,
    idLicitador int NOT NULL,
    idLeilao int NOT NULL,
    CONSTRAINT [PK_LICITACAO] PRIMARY KEY CLUSTERED ([id] ASC) WITH (IGNORE_DUP_KEY = OFF),
    CONSTRAINT [Licitacao_fk0] FOREIGN KEY ([idLicitador]) REFERENCES [Licitador]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION,
    CONSTRAINT [Licitacao_fk1] FOREIGN KEY ([idLeilao]) REFERENCES [Leilao]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO
CREATE TABLE [Fatura] (
    id INT NOT NULL UNIQUE,
    dataHora DATETIME NOT NULL,
    idLicitador INT NOT NULL,
    idLicitacao INT NOT NULL,
    idInstituicao INT NOT NULL,
    CONSTRAINT [PK_FATURA] PRIMARY KEY CLUSTERED ([id] ASC) WITH (IGNORE_DUP_KEY = OFF),
    CONSTRAINT [Fatura_fk0] FOREIGN KEY ([idLicitador]) REFERENCES [Licitador]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION,
    CONSTRAINT [Fatura_fk1] FOREIGN KEY ([idLicitacao]) REFERENCES [Licitacao]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION,
    CONSTRAINT [Fatura_fk2] FOREIGN KEY ([idInstituicao]) REFERENCES [Instituicao]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION
);

GO
CREATE TABLE [Notificacao] (
    idNotificacao int NOT NULL UNIQUE,
    mensagem varchar(200) NOT NULL,
    titulo varchar(300) NOT NULL,
    dataHora datetime NOT NULL,
    tipo int NOT NULL,
    idLicitador int NOT NULL,
	idLeilao int NOT NULL,
    CONSTRAINT [PK_NOTIFICACAO] PRIMARY KEY CLUSTERED ([idNotificacao] ASC) WITH (IGNORE_DUP_KEY = OFF),
    CONSTRAINT [Notificacao_fk0] FOREIGN KEY ([idLicitador]) REFERENCES [Licitador]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION,
	CONSTRAINT [Notificacao_fk1] FOREIGN KEY ([idLeilao]) REFERENCES [Leilao]([id]) ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO