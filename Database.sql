USE MovieWebsite
SELECT * From Celebrity
CREATE TABLE Menu
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(255),
	Link NVARCHAR(255),
	Meta VARCHAR(255) UNIQUE,
	Hide BIT,
	_ORDER INT,
	InitDate DATE,
)
CREATE TABLE SubMenu
(
	Id INT PRIMARY KEY IDENTITY,
	ParentId INT,
	Name NVARCHAR(255),
	Link NVARCHAR(255),
	Meta VARCHAR(255) UNIQUE,
	Hide BIT,
	_ORDER INT,
	InitDate DATE,
	FOREIGN KEY (ParentId) REFERENCES Menu(Id)
)
CREATE TABLE Movie
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(255),
	Wallpaper VARCHAR(255),
	Score DECIMAL(1),
	Type NVARCHAR(50) CHECK (Type in ('Theater', 'OnTV')),
	Summary NVARCHAR(MAX),
	Trailer NVARCHAR(255),
	ReleaseDate DATE,
	RunTime INT,

	Meta VARCHAR(255) UNIQUE,
	Hide BIT,
	_ORDER INT,
	InitDate DATE
)

CREATE TABLE Genre
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(255),

	Meta VARCHAR(255),
	Hide BIT,
	_ORDER INT,
	InitDate DATE
)
CREATE TABLE MovieGenre
(
    MovieId INT,
    GenreId INT,
    PRIMARY KEY (MovieId, GenreId),
    FOREIGN KEY (MovieId) REFERENCES Movie(Id) ON DELETE CASCADE,
    FOREIGN KEY (GenreId) REFERENCES Genre(Id) ON DELETE CASCADE
);
CREATE TABLE Celebrity
(
	Id INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(255),
	Avatar VARCHAR(255),
	CastImage VARCHAR(255),
	Role NVARCHAR(255),
	Country NVARCHAR(255),
	Bio NVARCHAR(MAX),
	FulBio NVARCHAR(MAX),
	Height INT,
	Birth DATE,

	Meta VARCHAR(255),
	Hide BIT,
	_ORDER INT,
	InitDate DATE
)
CREATE TABLE MovieCast
(
	MovieId INT,
    CelebId INT,
	Role NVARCHAR(255),
    PRIMARY KEY (MovieId, CelebId),
    FOREIGN KEY (MovieId) REFERENCES Movie(Id) ON DELETE CASCADE,
    FOREIGN KEY (CelebId) REFERENCES Celebrity(Id) ON DELETE CASCADE
)
CREATE TABLE News
(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(255),
	Image VARCHAR(255),
	Content NVARCHAR(MAX),
	Wallpaper VARCHAR(255),
	
	Meta VARCHAR(255) UNIQUE,
	Hide BIT,
	_ORDER INT,
	InitDate DATETIME
)
CREATE TABLE Users
(
	Id INT PRIMARY KEY IDENTITY,
	Username VARCHAR(255) UNIQUE NOT NULL,
	Password VARCHAR(255) NOT NULL,
	Email VARCHAR(255) UNIQUE NOT NULL,
	Avatar VARCHAR(255),
	FirstName NVARCHAR(255) NOT NULL,
	LastName NVARCHAR(255),
	Country NVARCHAR(255),
	Role NVARCHAR(100) NOT NULL DEFAULT 'User',
	Meta VARCHAR(255),
	Hide BIT,
	_ORDER INT,
	InitDate DATETIME
)
DROP TABLE Users
DROP TABLE MovieReview
DROP TABLE NewComment
DROP TABLE NewCommentReply
SELECT * From NEWS
CREATE TABLE MovieReview
(
	MovieId INT,
	UserId INT,
	Content NVARCHAR(MAX),
	Score INT,
	
	Meta VARCHAR(255),
	Hide BIT,
	_ORDER INT,
	InitDate DATETIME,
    PRIMARY KEY (MovieId, UserId),
    FOREIGN KEY (MovieId) REFERENCES Movie(Id) ON DELETE CASCADE,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
)
CREATE TABLE NewComment
(
	Id INT PRIMARY KEY IDENTITY,
	NewId INT,
	UserId INT,
	Content NVARCHAR(MAX),

	Meta VARCHAR(255),
	Hide BIT,
	_ORDER INT,
	InitDate DATETIME
	FOREIGN KEY (NewId) REFERENCES News(Id) ON DELETE CASCADE,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
)

CREATE TABLE NewCommentReply
(
	Id INT PRIMARY KEY IDENTITY,
	NewCommentId INT,
	UserId INT,
	Content NVARCHAR(MAX),

	Meta VARCHAR(255),
	Hide BIT,
	_ORDER INT,
	InitDate DATETIME
	FOREIGN KEY (NewCommentId) REFERENCES NewComment(Id) ON DELETE CASCADE,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
)

SELECT * From Users
INSERT INTO Users(Username, Password, Email, Avatar, FirstName, LastName, Country, Role) VALUES
('tuankiet106', '123', 'letuankiet123@gmail.com', 'userava2.jpg', N'Kiệt', N'Lê', N'Việt Nam', 'User'),
('tuankiet', '123', 'letuankiet@gmail.com', 'userava3.jpg', N'Tân', N'Phạm', N'Việt Nam', 'Staff')
INSERT INTO MovieReview(MovieId, UserId, Score, InitDate, Content) VALUES
(1, 1, 8, '2024-10-28', N'This is by far one of my favorite movies from the MCU. The introduction of new Characters both good and bad also makes the movie more exciting. giving the characters more of a back story can also help audiences relate more to different characters better, and it connects a bond between the audience and actors or characters. Having seen the movie three times does not bother me here as it is as thrilling and exciting every time I am watching it. In other words, the movie is by far better than previous movies (and I do love everything Marvel), the plotting is splendid (they really do out do themselves in each film, there are no problems watching it more than once.'),
(1, 2, 9, '2024-10-28', N'I can''t right much... it''s just so forgettable...Okay, from what I remember, I remember just sitting down on my seat and waiting for the movie to begin. 5 minutes into the movie, boring scene of Tony Stark just talking to his "dead" friends saying it''s his fault. 10 minutes in: Boring scene of Ultron and Jarvis having robot space battles(I dunno:/). 15 minutes in: I leave the theatre.2nd attempt at watching it: I fall asleep. What woke me up is the next movie on Netflix when the movie was over.')
INSERT INTO Menu(Name, Meta, Hide, _Order, InitDate)VALUES
('Home', 'trang-chu', 'False', 1, '2024-09-23'),
('Movie', 'movie', 'False', 1, '2024-09-23'),
('Celebrities', 'celebrity', 'False', 1, '2024-09-23'),
('News', 'new', 'False', 1, '2024-09-23')
INSERT INTO SubMenu(ParentId, Name, Link, Hide, _Order, InitDate) VALUES
(2, 'ACTION', '/movie?category=action', 'False', 1, GETDATE()),
(2, 'SCI-FI', '/movie?category=sci-fi', 'False', 1, GETDATE()),
(2, 'ADVANTURE', '/movie?category=advanture', 'False', 1, GETDATE()),
(2, 'THRILLER', '/movie?category=thriller', 'False', 1, GETDATE())


INSERT INTO Movie(Name, Wallpaper, Score, Type, Meta, Trailer, ReleaseDate, RunTime, Hide, _Order, InitDate, Summary) VALUES
('OBVILION', 'slider1.jpg', 7.5, 'Theater','obvilion', 'https://www.youtube.com/embed/1Q8fG0TtVAY','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('INTO THE WILD', 'slider2.jpg', 7.5, 'Theater','into-the-wild', 'https://www.youtube.com/embed/44LdLqgOpjo','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('BLADE RUNNER', 'slider3.jpg', 7.5, 'Theater','blade-runner', 'https://www.youtube.com/embed/e3Nl_TCQXuw','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('MULHOLLAND PRIDE', 'slider4.jpg', 7.5, 'Theater','mulholland-pride', 'https://www.youtube.com/embed/gbug3zTm3Ws','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('SKYFALL: EVIL OF BOSS', 'slider5.jpg', 7.5, 'Theater','skyfall-evil-of-boss', 'https://www.youtube.com/embed/e3Nl_TCQXuw','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('DIE HARD', 'slider6.jpg', 7.5, 'Theater','die-hard', 'https://www.youtube.com/embed/1Q8fG0TtVAY','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('THE WALK', 'slider1.jpg', 7.5, 'Theater','the-walk', 'https://www.youtube.com/embed/44LdLqgOpjo','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('THE REVENANT', 'slider2.jpg', 7.5, 'Theater','the-revenant', 'https://www.youtube.com/embed/gbug3zTm3Ws','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('INTERSTELLAR', 'slider3.jpg', 7.5, 'Theater','interstellar', 'https://www.youtube.com/embed/e3Nl_TCQXuw','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('OBVILION', 'slider4.jpg', 7.5, 'Theater','obvillion2', 'https://www.youtube.com/embed/44LdLqgOpjo','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('INTO THE WILD', 'slider5.jpg', 7.5, 'Theater','into-the-wild2', 'https://www.youtube.com/embed/1Q8fG0TtVAY','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('BLADE RUNNER', 'slider6.jpg', 7.5, 'Theater','blade-runner2', 'https://www.youtube.com/embed/gbug3zTm3Ws','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.')
('INTERSTELLAR', 'slider3.jpg', 7.5, 'Theater','interstellar2', 'https://www.youtube.com/embed/e3Nl_TCQXuw','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('OBVILION', 'slider4.jpg', 7.5, 'Theater','obvillion3', 'https://www.youtube.com/embed/44LdLqgOpjo','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('INTO THE WILD', 'slider5.jpg', 7.5, 'Theater','into-the-wild3', 'https://www.youtube.com/embed/1Q8fG0TtVAY','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.'),
('BLADE RUNNER', 'slider6.jpg', 7.5, 'Theater','blade-runner3', 'https://www.youtube.com/embed/gbug3zTm3Ws','2015-5-1', 141, 'False', 1, '2024-09-23',N'Tony Stark creates the Ultron Program to protect the world, but when the peacekeeping program becomes hostile, The Avengers go into action to try and defeat a virtually impossible enemy together. Earth''s mightiest heroes must come together once again to protect the world from global extinction.')
SELECT * From Movie

INSERT INTO Genre(Name, Hide, _Order, InitDate) VALUES
('SCI-FI', 0, 1, '2024-10-22'),
('ACTION', 0, 2, '2024-10-22'),
('COMEDY', 0, 3, '2024-10-22'),
('ADVANTURE', 0, 4, '2024-10-22'),
('THRILLER', 0, 5, '2024-10-22');
SELECT * From Genre
INSERT INTO MovieGenre(MovieId, GenreId) VALUES
(1, 1), (1, 2), (2, 4), (3, 2), (4, 5), (5, 1)
SELECT * From MovieGenre
INSERT INTO Celebrity(Name, Avatar, CastImage, Role, Country, Height, Birth, Meta, Hide, _ORDER, InitDate, Bio, FulBio) VALUES
('Samuel N.Jack', 'ceb1.jpg', 'cast1.jpg', 'Actor', N'Việt Nam', 175, '2004-06-10', 'samnuel-n-jack', 'False', 1, '2024-09-23', 'Hugh Michael Jackman is an Australian actor, singer, multi-instrumentalist, dancer and producer. Jackman has won international recognition for his roles in major films, notably as superhero, period, and romance characters.', 'Hugh Michael Jackman is an Australian actor, singer, multi-instrumentalist, dancer and producer. Jackman has won international recognition for his roles in major films, notably as superhero, period, and romance characters.'),
('Benjamin Carroll', 'ceb2.jpg', 'cast2.jpg', 'Actor', N'Việt Nam', 175, '2004-06-10', 'benjamin-carroll', 'False', 1, '2024-09-23', 'Hugh Michael Jackman is an Australian actor, singer, multi-instrumentalist, dancer and producer. Jackman has won international recognition for his roles in major films, notably as superhero, period, and romance characters.', 'Hugh Michael Jackman is an Australian actor, singer, multi-instrumentalist, dancer and producer. Jackman has won international recognition for his roles in major films, notably as superhero, period, and romance characters.'),
('Beverly Griffin', 'ceb3.jpg', 'cast3.jpg', 'Actor', N'Việt Nam', 175, '2004-06-10', 'beverly-griffin', 'False', 1, '2024-09-23', 'Hugh Michael Jackman is an Australian actor, singer, multi-instrumentalist, dancer and producer. Jackman has won international recognition for his roles in major films, notably as superhero, period, and romance characters.', 'Hugh Michael Jackman is an Australian actor, singer, multi-instrumentalist, dancer and producer. Jackman has won international recognition for his roles in major films, notably as superhero, period, and romance characters.'),
('Justin Weaver', 'ceb4.jpg', 'cast4.jpg', 'Actor', N'Việt Nam', 175, '2004-06-10', 'justin-weaver', 'False', 1, '2024-09-23', 'Hugh Michael Jackman is an Australian actor, singer, multi-instrumentalist, dancer and producer. Jackman has won international recognition for his roles in major films, notably as superhero, period, and romance characters.', 'Hugh Michael Jackman is an Australian actor, singer, multi-instrumentalist, dancer and producer. Jackman has won international recognition for his roles in major films, notably as superhero, period, and romance characters.')
SELECT * From Celebrity
INSERT INTO MovieCast(MovieId, CelebId, Role) VALUES
(1, 1, N'Bùi Lê Phát Hải'), (1, 2, N'Bronson Peary'), (1,3, N'Hugh Jackman'), (1, 4, N'Wolverine'), (2, 1, N'Robert'), (3, 1, N'Logan Paul')
INSERT INTO News(Title, Image, Content, Meta, Hide, _Order, InitDate) VALUES
('Brie Larson to play first female white house candidate Victoria Woodull in Amazon film', 'blog-it1.jpg', 'testContent', 'brie-larson-to-play-first-female-white-house-candidate-victoria-woodull-in-amazon-film','FALSE', 1, '2024-10-16 15:00:00'),
('Brie Larson to play first female white house candidate Victoria Woodull in Amazon film', 'blog-it1.jpg', 'testContent', 'brie-larson-to-play-first-female-white-house-candidate-victoria-woodull-in-amazon-film1','FALSE', 1, '2024-10-16 15:00:00'),
('Brie Larson to play first female white house candidate Victoria Woodull in Amazon film', 'blog-it1.jpg', 'testContent', 'brie-larson-to-play-first-female-white-house-candidate-victoria-woodull-in-amazon-film2','FALSE', 1, '2024-10-16 15:00:00'),
('Brie Larson to play first female white house candidate Victoria Woodull in Amazon film', 'blog-it1.jpg', 'testContent', 'brie-larson-to-play-first-female-white-house-candidate-victoria-woodull-in-amazon-film3','FALSE', 1, '2024-10-16 15:00:00'),
('Brie Larson to play first female white house candidate Victoria Woodull in Amazon film', 'blog-it1.jpg', 'testContent', 'brie-larson-to-play-first-female-white-house-candidate-victoria-woodull-in-amazon-film4','FALSE', 1, '2024-10-16 15:00:00');
INSERT INTO NewComment(NewId, UserId, Content, InitDate) VALUES
(1, 1, N'Even though Journey''s classic vocalist Steve Perry didn''t reunite with the band during their Rock Hall performance (to the dismay of hopeful fans), he did offer up a touching speech.', GETDATE()),
(1, 2, N'Joan Baez was the sharpest of the Rock Hall inductees, singing about deportees and talking social activism as well as joking about her age and the likelihood that a good portion of the Barclays.', GETDATE())
INSERT INTO NewCommentReply(NewCommentId, UserId, Content, InitDate) VALUES
(1, 1, N'Prince died not long after the 2016 Rock Hall ceremony, so this year''s edition featured Lenny Kravitz and a full gospel choir performing a swamp-funk take on When Doves Cry.', GETDATE()),
(1, 2, N'Blue Sky Studios is one of the world’s leading digital animation movie studios and we are proud of their commitment to stay and grow in Connecticut.', GETDATE())

UPDATE FROM News
SET 