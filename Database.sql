USE MovieWebsite
SELECT * From Menu
CREATE TABLE Movie
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(255),
	Wallpaper VARCHAR(255),
	Score DECIMAL(1),
	Type NVARCHAR(50) CHECK (Type in ('Theater', 'OnTV')),

	Meta VARCHAR(255),
	Hide BIT,
	_ORDER INT,
	InitDate DATE,
)
CREATE TABLE Celebrity
(
	Id INT PRIMARY KEY IDENTITY,
	NAME NVARCHAR(255),
	Avatar VARCHAR(255),
	Role NVARCHAR(255),
	
	Meta VARCHAR(255),
	Hide BIT,
	_ORDER INT,
	InitDate DATE
)
INSERT INTO Movie VALUES
('Test1', 'mv1.jpg', 7.5, '', 'False', 1, '2024-09-23', 'Theater'),
('Test2', 'mv2.jpg', 7.0, '', 'False', 1, '2024-09-23', 'Theater'),
('Test3', 'mv3.jpg', 7.0, '', 'False', 1, '2024-09-23', 'Theater'),
('Test4', 'mv4.jpg', 7.0, '', 'False', 1, '2024-09-23', 'Theater'),
('Test5', 'mv5.jpg', 7.0, '', 'False', 1, '2024-09-23', 'Theater'),
('Test6', 'mv1.jpg', 7.0, '', 'False', 1, '2024-09-23', 'Theater'),
('Test7', 'mv2.jpg', 7.0, '', 'False', 1, '2024-09-23', 'Theater'),
('Test8', 'mv3.jpg', 7.0, '', 'False', 1, '2024-09-23', 'Theater'),
('Test9', 'mv4.jpg', 7.0, '', 'False', 1, '2024-09-23', 'Theater'),
('Test10', 'mv5.jpg', 7.0, '', 'False', 1, '2024-09-23', 'Theater'),
('Test11', 'mv1.jpg', 7.0, '', 'False', 1, '2024-09-23', 'Theater'),
('Test12', 'mv2.jpg', 7.0, '', 'False', 1, '2024-09-23', 'Theater')
INSERT INTO Celebrity VALUES
('Samuel N.Jack', 'ceb1.jpg', 'Actor', '', 'False', 1, '2024-09-23'),
('Benjamin Carroll', 'ceb2.jpg', 'Actor', '', 'False', 1, '2024-09-23'),
('Beverly Griffin', 'ceb3.jpg', 'Actor', '', 'False', 1, '2024-09-23'),
('Justin Weaver', 'ceb4.jpg', 'Actor', '', 'False', 1, '2024-09-23')