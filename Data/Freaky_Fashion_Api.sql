CREATE TABLE Products (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL,
	Description NVARCHAR(100),
	Photo NVARCHAR(60),
	Label NVARCHAR(50),
	SKU NVARCHAR(50),
	Price INT NOT NULL,
	Kategori INT,
	FOREIGN KEY (Kategori) REFERENCES Categories(Id)
);

INSERT INTO Products (Name, Description, Photo, Label, SKU, Price, Kategori)
VALUES
('Svart-T-Shirt', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit amet, consectetur adipisicing elit', 'https://placehold.co/300x400/grey/white?text=Svart-T-Shirt', 'Levis', 'AAA111', 199, 1),
('Vit-T-Shirt', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit amet, consectetur adipisicing elit', 'https://placehold.co/300x400/grey/white?text=Vit-T-Shirt', 'Levis', 'BBB111', 199, 1),
('Gul-T-Shirt', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit amet, consectetur adipisicing elit', 'https://placehold.co/300x400/grey/white?text=Gul-T-Shirt', 'Levis', 'CCC111', 199, 1),
('Orange-Skor', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit amet, consectetur adipisicing elit', 'https://placehold.co/300x400/grey/white?text=Orange-Skor', 'Levis', 'DDD111', 199, 2),
('Brun-Skor', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit amet, consectetur adipisicing elit', 'https://placehold.co/300x400/grey/white?text=Brun-Skor', 'Levis', 'EEE111', 199, 2),
('Lilla-Scarf', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit amet, consectetur adipisicing elit', 'https://placehold.co/300x400/grey/white?text=Lilla-Scarf', 'Levis', 'FFF111', 199, 3);

CREATE TABLE Categories (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL,
	Image NVARCHAR(60),
	Slug NVARCHAR(50),
);

INSERT INTO Categories (Id, Name, Image, Slug, Products)
VALUES
(1, 'kläder', 'https://placehold.co/300x400/grey/white?text=Kläder', 'kläder'),
(2, 'skor', 'https://placehold.co/300x400/grey/white?text=Skor', 'skor'),
(3, 'accessoarer', 'https://placehold.co/300x400/grey/white?text=Accessoarer', 'accessoarer');

