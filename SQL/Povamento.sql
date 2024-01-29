-- USE RhythmsOfGiving

-- Licitador
INSERT INTO [Licitador] (id, nome, palavraPasse, dataNascimento, nrCartao, email, nif, numeroCC)
VALUES
(1, 'Diogo Matos', 'senha123', '2003-03-29', 123456789, 'diogo.matos@email.com', 247710350, 30590916),
(2, 'João Silva', 'senha456', '1990-05-15', 123456789, 'joao.silva@email.com', 123456789, 987654321),
(3, 'Maria Santos', 'senha789', '1985-08-22', 987654321, 'maria.santos@email.com', 987654321, 123456789);

-- Administrador
INSERT INTO [Administrador] (id, palavraPasse, email)
VALUES
(1, 'admin', 'admin@email.com');

-- Instituicao
INSERT INTO [Instituicao] (id, nome, descricao, logotipo, hiperligacao, iban, idAdministrador)
VALUES
(1, 'The Trevor Project',
'The Trevor Project é uma organização sem fins lucrativos norte-americana fundada em 1998 em West Hollywood, Califórnia, com o objetivo de informar e prevenir o suicído entre jovens LGBT, incluindo outros queer.',
'https://mma.prnewswire.com/media/1636688/ttp_logo_primary_tagline_rgb_Logo.jpg?p=twitter',
'https://www.thetrevorproject.org/', 'PT50 1234 5678 9012 3456 7890 1', 1),
(2, 'Cruz Vermelha Portuguesa', 'A Cruz Vermelha tem vindo a apostar nas actividades de prevenção e preparação para catástrofes.', 
'https://upload.wikimedia.org/wikipedia/commons/thumb/9/95/Logo_CVP.svg/1200px-Logo_CVP.svg.png',
'https://www.cruzvermelha.pt/', 'PT50 2345 6789 0123 4567 8901 2', 1),
(3, 'Make-A-Wish Portugal', 
'A missão da Make-A-Wish é a realização de desejos a crianças e jovens, dos 3 aos 17 anos, em todo o território...',
'https://worldwish.org/wp-content/uploads/2022/01/make-a-wish-vector-logo.png',
'https://makeawish.pt/', 'PT50 1234 5678 9012 3456 7890 2', 1);

-- GeneroMusical
INSERT INTO [GeneroMusical] (id, nome, idAdministrador)
VALUES
(1, 'Pop', 1),
(2, 'Rock', 1),
(3, 'Hip Hop', 1),
(4, 'Rap', 1),
(5, 'Jazz', 1),
(6, 'Blues', 1),
(7, 'Funk', 1),
(8, 'Soul', 1),
(9, 'Reggae', 1),
(10, 'Fado', 1),
(11, 'House', 1),
(12, 'R&B', 1),
(13, 'Country', 1),
(14, 'Indie', 1),
(15, 'Metal', 1),
(16, 'Clássica', 1);

-- Artista
INSERT INTO [Artista] (id, imagem, nome, idAdministrador)
VALUES
(1, 'https://i.scdn.co/image/ab67616100005174859e4c14fa59296c8649e0e4', 'Taylor Swift', 1),
(2, 'https://images.impresa.pt/expresso/2022-01-19-harry-styles/original', 'Harry Styles', 1),
(3, 'https://i.scdn.co/image/ab6761610000e5eb68f6e5892075d7f22615bd17', 'Adele', 1);

-- Leilao
INSERT INTO [Leilao] (id, titulo, valorAtual, valorMinimo, dataHoraFinal, localizacao, estado, tipoLeilao, descricao, imagem, idArtista, idGeneroMusical, idAdministrador)
VALUES
(1, 'Taylor Swift, The Eras Tour: Exclusive VIP Seating', 800.00, 500.00, '2024-02-15 18:00:00', 'Lisboa', 1,
'ingles', 'Participe neste emocionante leilão e tenha a oportunidade de experienciar o espetáculo mais esperado do ano de uma forma que nunca imaginou possível. Para além de desfrutar de uma vista privilegiada e desobstruída do palco, será tratado como uma verdadeira celebridade. O seu acesso VIP inclui uma entrada exclusiva para o bar, onde poderá degustar os melhores cocktails e petiscos enquanto se prepara para uma noite inesquecível. Sinta-se como parte integrante do espetáculo, enquanto Taylor Swift hipnotiza o público com seus êxitos lendários e faixas do seu último álbum.',
'https://assets.teenvogue.com/photos/641b2a23912ddccbabf80f80/16:9/w_2560%2Cc_limit/GettyImages-1474459622.jpg', 1, 1, 1),
(2, 'Love On Tour: Exclusive Meet-Up', 1100.00, 900.00, '2024-02-20 20:00:00', 'Porto', 1, 'asCegas', 'Viva uma experiência única e exclusiva num espetáculo de um dos maiores nomes na indústria musical. Tenha acesso a uma visão total e desobstruída para o palco e ainda um encontro exclusivo com o artista pós-espetáculo.',
'https://images.impresa.pt/expresso/2023-02-12-harry-styles.jpg-62d6c4d8/original', 2, 14, 1),
(3, 'One Night Only: Private Concert', 959.00, 600.00, '2024-03-10 21:00:00', 'Porto', 1, 'ingles',
'Viva um momento inesquecível, tenha acesso a um espetáculo privado com a incrível Adele. A oferta inclui um luxuoso almoço e jantar. Traga até 10 amigos ou familiares.',
'https://hips.hearstapps.com/hmg-prod/images/revealed-an-extended-preview-and-first-look-of-the-news-photo-1636947501.jpg?crop=0.668xw:1.00xh;0.209xw,0&resize=640:*',
3, 1, 1);

-- Notificação
INSERT INTO [Notificacao] (idNotificacao, mensagem, titulo, dataHora, tipo, idLicitador, idLeilao)
VALUES
(1, 'O pagamento para "One Night Only: Private Concert" foi processado com sucesso.', 'A sua licitação venceu!', '2024-01-10 21:30:00', 1, 1 , 3);

-- Licitacao
INSERT INTO [Licitacao] (id, dataHora, valor, idLicitador, idLeilao)
VALUES
(1, '2024-02-10 12:30:00', 800.00, 1, 1),
(2, '2024-01-10 20:30:00', 959.00, 1, 3),
(3, '2024-02-11 14:21:00', 1100.00, 2, 2);

-- Fatura
INSERT INTO [Fatura] (id, dataHora, idLicitador, idLicitacao, idInstituicao)
VALUES
(1, '2024-01-10 22:00:00', 1, 2, 1);

-- SELECT * FROM [Licitador]
-- SELECT * FROM [Administrador]
-- SELECT COUNT(*) AS TotalRows FROM Licitador
-- SELECT * FROM Licitacao
-- SELECT * FROM Leilao
-- SELECT * FROM Instituicao

CREATE PROCEDURE CriarAdmin
    @email varchar(100),
    @palavraPasse varchar(45)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @novoAdministradorID int

    -- Obter o próximo ID disponível
    SELECT @novoAdministradorID = ISNULL(MAX(id), 0) + 1 FROM Administrador

    -- Inserir novo administrador
    INSERT INTO Administrador (id, email, palavraPasse)
    VALUES (@novoAdministradorID, @email, @palavraPasse)

END;
