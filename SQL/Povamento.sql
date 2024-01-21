USE RhythmsOfGiving2

-- Licitador
INSERT INTO [Licitador] (id, nome, palavraPasse, dataNascimento, nrCartao, email, nif, numeroCC)
VALUES
(1, 'João Silva', 'senha123', '1990-05-15', 123456789, 'joao.silva@email.com', 123456789, 987654321),
(2, 'Maria Santos', 'senha456', '1985-08-22', 987654321, 'maria.santos@email.com', 987654321, 123456789);

-- Administrador
INSERT INTO [Administrador] (id, palavraPasse, email)
VALUES
(1, 'admin123', 'admin@email.com');

-- Instituicao
INSERT INTO [Instituicao] (id, nome, descricao, logotipo, hiperligacao, iban, idAdministrador)
VALUES
(1, 'Instituição A', 'Descrição da Instituição A', 'logo_inst_a.png', 'http://www.instituicaoA.com', 'PT50 1234 5678 9012 3456 7890 1', 1),
(2, 'Instituição B', 'Descrição da Instituição B', 'logo_inst_b.png', 'http://www.instituicaoB.com', 'PT50 2345 6789 0123 4567 8901 2', 1);

-- GeneroMusical
INSERT INTO [GeneroMusical] (id, nome, idAdministrador)
VALUES
(1, 'Rock', 1),
(2, 'Pop', 1);

-- Artista
INSERT INTO [Artista] (id, imagem, nome, idAdministrador)
VALUES
(1, 'artista1.jpg', 'Artista A', 1),
(2, 'artista2.jpg', 'Artista B', 1);

-- Leilao
INSERT INTO [Leilao] (id, titulo, valorAtual, valorMinimo, dataHoraFinal, localizacao, estado, tipoLeilao, descricao, imagem, idArtista, idGeneroMusical, idAdministrador, idInstituicao)
VALUES
(1, 'Leilão de Arte', 500.00, 100.00, '2024-02-15 18:00:00', 'Galeria A', 1, 'Arte', 'Leilão de obras de arte contemporânea', 'leilao_arte.jpg', 1, 1, 1, 1),
(2, 'Leilãoo de Música', 200.00, 50.00, '2024-02-20 20:00:00', 'Teatro B', 1, 'Música', 'Leilãoo de instrumentos musicais e memorabilia', 'leilao_musica.jpg', 2, 2, 1, 2);

-- Notificação
INSERT INTO [Notificacao] (idNotificacao, mensagem, titulo, dataHora, tipo, idLicitador, idLeilao)
VALUES
(1, 'Nova notificação', 'Aviso', '2024-01-19 10:30:00', 1, 1 , 1),
(2, 'Leilão em andamento', 'Alerta', '2024-01-20 15:45:00', 2, 2 , 1);
-- Licitacao
INSERT INTO [Licitacao] (id, dataHora, valor, idLicitador, idLeilao)
VALUES
(1, '2024-02-10 12:30:00', 150.00, 1, 1),
(2, '2024-02-18 16:45:00', 80.00, 2, 2);

-- Fatura
INSERT INTO [Fatura] (id, dataHora, idLicitador, idLicitacao, idInstituicao)
VALUES
(1, '2024-02-20 22:00:00', 1, 1, 1),
(2, '2024-02-22 10:15:00', 2, 2, 2);

SELECT * FROM [Licitador]
SELECT * FROM [Administrador]
SELECT COUNT(*) AS TotalRows FROM Licitador
SELECT * FROM Licitacao
SELECT * FROM Leilao
