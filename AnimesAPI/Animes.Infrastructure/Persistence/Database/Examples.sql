USE AnimeDatabase;
GO

-- Adicionar Exemplos para poder testar a API manualmente


-- =============================================
-- Inserir dados na tabela Directors
-- =============================================
PRINT 'Inserindo Diretores...'
INSERT INTO Directors (Name) VALUES
('Hayao Miyazaki'),
('Makoto Shinkai'),
('Hideaki Anno'),
('Shinichiro Watanabe'),
('Tetsurō Araki'),
('Gorō Taniguchi'),
('Yoshiyuki Tomino');
GO

-- =============================================
-- Inserir dados na tabela Studios
-- =============================================
PRINT 'Inserindo Estúdios...'
INSERT INTO Studios (Name) VALUES
('Studio Ghibli'),
('CoMix Wave Films'),
('Gainax'),
('Sunrise'),
('Wit Studio'),
('MAPPA'),
('Madhouse');
GO

-- =============================================
-- Inserir dados na tabela Genres
-- =============================================
PRINT 'Inserindo Gêneros...'
INSERT INTO Genres (Name) VALUES
('Aventura'),
('Fantasia'),
('Drama'),
('Ficção Científica'),
('Mecha'),
('Ação'),
('Romance'),
('Slice of Life'),
('Suspense'),
('Pós-apocalíptico');
GO

-- =============================================
-- Inserir dados na tabela Animes
-- =============================================
-- Nota: Os IDs de Diretor e Estúdio são baseados na ordem de inserção acima.
-- Ex: Hayao Miyazaki = 1, Studio Ghibli = 1
PRINT 'Inserindo Animes...'
INSERT INTO Animes (Name, Summary, ReleaseYear, EpisodeCount, Score, DirectorId, StudioId) VALUES
('A Viagem de Chihiro', 'Chihiro e seus pais estão se mudando para uma cidade diferente. No caminho, eles se deparam com um túnel que os leva a um mundo mágico e perigoso.', 2001, 1, 9.2, 1, 1),
('Your Name', 'Mitsuha, uma garota do campo, e Taki, um garoto de Tóquio, começam a trocar de corpo misteriosamente em seus sonhos.', 2016, 1, 9.1, 2, 2),
('Neon Genesis Evangelion', 'Em um mundo pós-apocalíptico, o jovem Shinji Ikari é recrutado para pilotar um robô gigante (EVA) e lutar contra seres monstruosos chamados Anjos.', 1995, 26, 8.5, 3, 3),
('Cowboy Bebop', 'A tripulação da nave Bebop viaja pelo sistema solar em busca de recompensas. Uma mistura de ficção científica, faroeste e noir.', 1998, 26, 8.9, 4, 4),
('Attack on Titan', 'A humanidade vive dentro de cidades cercadas por enormes muralhas para se proteger de titãs, criaturas humanoides gigantes que devoram humanos sem motivo aparente.', 2013, 87, 9.0, 5, 5),
('Code Geass', 'Em um Japão conquistado pelo Império de Britannia, o estudante Lelouch vi Britannia ganha um poder misterioso chamado Geass e lidera uma rebelião.', 2006, 50, 8.7, 6, 4),
('Death Note', 'Light Yagami, um estudante genial, encontra um caderno sobrenatural que permite matar qualquer pessoa cujo nome seja escrito nele.', 2006, 37, 8.6, 5, 7);
GO

-- =============================================
-- Inserir dados na tabela de junção Anime_Genres
-- =============================================
-- Nota: Os IDs são baseados na ordem de inserção das tabelas Animes e Genres.
-- Ex: A Viagem de Chihiro = 1, Aventura = 1, Fantasia = 2
PRINT 'Relacionando Animes e Gêneros...'
INSERT INTO Anime_Genres (AnimeId, GenreId) VALUES
-- A Viagem de Chihiro (Aventura, Fantasia, Drama)
(1, 1),
(1, 2),
(1, 3),

-- Your Name (Romance, Drama, Fantasia)
(2, 7),
(2, 3),
(2, 2),

-- Neon Genesis Evangelion (Mecha, Ficção Científica, Drama, Pós-apocalíptico)
(3, 5),
(3, 4),
(3, 3),
(3, 10),

-- Cowboy Bebop (Ação, Aventura, Ficção Científica)
(4, 6),
(4, 1),
(4, 4),

-- Attack on Titan (Ação, Fantasia, Drama, Pós-apocalíptico)
(5, 6),
(5, 2),
(5, 3),
(5, 10),

-- Code Geass (Ação, Mecha, Ficção Científica, Suspense)
(6, 6),
(6, 5),
(6, 4),
(6, 9),

-- Death Note (Suspense, Drama)
(7, 9),
(7, 3);
GO

PRINT 'Script de inserção concluído com sucesso!'
GO