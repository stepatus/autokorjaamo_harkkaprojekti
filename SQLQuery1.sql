CREATE TABLE ASIAKKAAT (id INTEGER IDENTITY (1,1) PRIMARY KEY, nimi VARCHAR(50),puhelinnro VARCHAR(50));

CREATE TABLE VARAOSAT (id INTEGER IDENTITY (1,1) PRIMARY KEY, nimi text, hinta INTEGER);

CREATE TABLE AUTOT(ID INTEGER IDENTITY (1,1) PRIMARY KEY, merkki VARCHAR(50), malli Varchar(50), vuosimalli INTEGER);

CREATE TABLE HUOLTOTILAUS(id INTEGER IDENTITY (1,1) PRIMARY KEY, asiakas_id INTEGER REFERENCES ASIAKKAAT ON DELETE CASCADE, varosa_id INTEGER REFERENCES VARAOSAT ON DELETE CASCADE,auto_id INTEGER REFERENCES AUTOT ON DELETE CASCADE, toimitettu BIT DEFAULT 0);

ALTER TABLE AUTOT ADD auton_ik� INTEGER 

Drop TABLE AUTOT

SELECT ASIAKKAAT.id as id, ASIAKKAAT.nimi AS nimi, ASIAKKAAT.puhelinnro AS puhelinnumero, VARAOSAT.nimi AS varaosan_nimi, VARAOSAT.hinta AS hinta, AUTOT.merkki AS merkki, AUTOT.malli AS malli, HUOLTOTILAUS.toimitettu AS KUITTI FROM VARAOSAT, ASIAKKAAT, AUTOT, HUOLTOTILAUS

select * from VARAOSAT

INSERT INTO HUOLTOTILAUS (asiakas_id, auto_id, varosa_id) VALUES(1,1,1);