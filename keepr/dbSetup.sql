CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS keeps (
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) NOT NULL COMMENT 'Keep Name',
  description varchar(255) COMMENT 'Keep Description',
  img varchar(255) DEFAULT 'https://placehold.it/200x200' COMMENT 'Image Url',
  -- how to make the ints default to zero?  Do I just send that over from the client side? Putting in DEFAULT 0 did not work
  views int COMMENT 'Number of views',
  shares int COMMENT 'Number of shares',
  keeps int COMMENT 'Number of downloads?',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Account Id for creator',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults (
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) NOT NULL COMMENT 'Keep Name',
  description varchar(255) COMMENT 'Keep Description',
  isPrivate TINYINT COMMENT 'Is it a private vault?',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Account Id for creator',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaultKeeps (
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Account Id for creator',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  vaultId INT NOT NULL COMMENT 'Vauld Id',
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  keepId INT NOT NULL COMMENT 'Keep Id',
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';