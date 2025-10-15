CREATE TABLE Products (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL,
	Description NVARCHAR(100),
	Photo NVARCHAR(60),
	Label NVARCHAR(50),
	SKU NVARCHAR(50),
	Price INT NOT NULL,
	Kategori NVARCHAR(50)
);

INSERT INTO Products (Name, Description, Photo, Label, SKU, Price, Kategori)
VALUES
('Svart-T-Shirt', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit amet, consectetur adipisicing elit', 'https://placehold.co/300x400/grey/white?text=Svart-T-Shirt', 'Levis', 'AAA111', 199, 'kläder'),
('Vit-T-Shirt', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit amet, consectetur adipisicing elit', 'https://placehold.co/300x400/grey/white?text=Vit-T-Shirt', 'Levis', 'BBB111', 199, 'kläder'),
('Gul-T-Shirt', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit amet, consectetur adipisicing elit', 'https://placehold.co/300x400/grey/white?text=Gul-T-Shirt', 'Levis', 'CCC111', 199, 'kläder');