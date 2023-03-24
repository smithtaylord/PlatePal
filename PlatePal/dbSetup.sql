CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    recipes(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        title VARCHAR(100) NOT NULL,
        instructions TEXT NOT NULL,
        img VARCHAR(500),
        category VARCHAR(100),
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

INSERT INTO
    recipes (
        title,
        instructions,
        img,
        category,
        `creatorId`
    )
VALUES (
        'Spa-Getty',
        'Noodles and sauce',
        'https://thumbs.dreamstime.com/z/flying-spaghetti-monster-young-religion-significant-amount-humor-flying-spagetty-monster-fsm-187313812.jpg',
        'noodles',
        '641b5f51c659f88d558161f9'
    );

SELECT rec.*, acct.*
FROM recipes rec
    JOIN accounts acct ON rec.creatorId = acct.id;

CREATE TABLE
    ingredients(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(255) NOT NULL,
        quantity VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

INSERT INTO
    ingredients (name, quantity, recipeId)
VALUES ('noodles', '7 cups', 11);

SELECT ing.*
FROM ingredients ing
    JOIN recipes rec ON rec.id = recipeId
WHERE recipeId = 26;