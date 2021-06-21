create database family_budget_management;

CREATE TABLE operations (
	id INT NOT NULL AUTO_INCREMENT,
	sum DECIMAL NOT NULL,
	type ENUM('+', '-') NOT NULL,
	date DATE NOT NULL,
	PRIMARY KEY (id)
);