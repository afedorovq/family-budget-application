create database family_budget_management;

CREATE TABLE operations(
	id INT NOT NULL AUTO_INCREMENT,
	sum DECIMAL NOT NULL,
	type ENUM('+', '-') NOT NULL,
	date DATE NOT NULL,
	category_type VARCHAR(54),
	PRIMARY KEY (id),
	FOREIGN KEY(category_type) REFERENCES categories(category)
);

CREATE TABLE categories(
	id INT NOT NULL AUTO_INCREMENT,
	category VARCHAR(50) UNIQUE,
	PRIMARY KEY (id)
);