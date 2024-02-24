CREATE SCHEMA skyvault;

CREATE  TABLE skyvault.customer_profiles ( 
	id                   INT    NOT NULL AUTO_INCREMENT  PRIMARY KEY,
	salutation_id        INT    NOT NULL   ,
	preferred_comm_id    INT    NOT NULL   ,
	system_user_id       INT    NOT NULL   ,
	parent_id            INT
 ) engine=InnoDB;

CREATE  TABLE skyvault.frequent_flyer_numbers ( 
	id                   INT    NOT NULL AUTO_INCREMENT  PRIMARY KEY,
	flyer_number        VARCHAR(50)    NOT NULL   ,
	customer_profile_id          INT    NOT NULL  
 ) engine=InnoDB;

CREATE  TABLE skyvault.jobs ( 
	id                   INT    NOT NULL AUTO_INCREMENT  PRIMARY KEY,
	date_time            DATE    NOT NULL   ,
	`status`             CHAR(1)    NOT NULL   ,
	customer_profile_id          INT    NOT NULL   ,
	template_id          INT    NOT NULL   ,
	log                  TEXT 
 ) engine=InnoDB;


CREATE  TABLE skyvault.notification_templates ( 
	id                   INT    NOT NULL AUTO_INCREMENT  PRIMARY KEY,
	content              LONGTEXT       ,
	notification_type    INT    NOT NULL   ,
	file                 LONGTEXT
 ) engine=InnoDB;

CREATE  TABLE skyvault.notification_types ( 
	id                   INT    NOT NULL AUTO_INCREMENT  PRIMARY KEY,
	type_name            VARCHAR(100)    NOT NULL   
 ) engine=InnoDB;

CREATE  TABLE skyvault.passports ( 
	id                   INT    NOT NULL AUTO_INCREMENT  PRIMARY KEY,
	customer_profile_id          INT    NOT NULL   ,
	last_name            VARCHAR(100)    NOT NULL   ,
	other_names          VARCHAR(100)       ,
	passport_number      VARCHAR(100)    NOT NULL   ,
	gender               VARCHAR(50)    NOT NULL   ,
	date_of_birth        DATE    NOT NULL   ,
	place_of_birth       VARCHAR(100)       ,
	nationality_id       INT    NOT NULL   ,
	is_primary           CHAR(1)  DEFAULT (0)  NOT NULL
 ) engine=InnoDB;

CREATE  TABLE skyvault.salutations ( 
	id                   INT    NOT NULL AUTO_INCREMENT  PRIMARY KEY,
	salutation_name      VARCHAR(100)    NOT NULL   
 ) engine=InnoDB;

CREATE  TABLE skyvault.system_users ( 
	id                   INT    NOT NULL AUTO_INCREMENT  PRIMARY KEY,
	first_name           VARCHAR(100)    NOT NULL   ,
	last_name            VARCHAR(100)    NOT NULL   ,
	user_role            VARCHAR(10)    NOT NULL   ,
	sam_profile_id       VARCHAR(50)    NOT NULL   ,
	profile_picture      TEXT       ,
	active               CHAR(1)  DEFAULT (1)     
 ) engine=InnoDB;

CREATE  TABLE skyvault.visas ( 
	id                   INT    NOT NULL AUTO_INCREMENT  PRIMARY KEY,
	visa_number          VARCHAR(100)    NOT NULL   ,
	issued_place         VARCHAR(100)    NOT NULL   ,
	issued_date          DATE    NOT NULL   ,
	expire_date          DATE    NOT NULL   ,
	country_id           INT    NOT NULL   ,
	passport_id          INT    NOT NULL
 ) engine=InnoDB;

CREATE  TABLE skyvault.communication_methods ( 
	id                   INT    NOT NULL AUTO_INCREMENT  PRIMARY KEY,
	comm_title           VARCHAR(50)    NOT NULL   
 ) engine=InnoDB;

CREATE  TABLE skyvault.countries ( 
	id                   INT    NOT NULL AUTO_INCREMENT  PRIMARY KEY,
	country_code         VARCHAR(5)       ,
	country_name         VARCHAR(100)    NOT NULL   
 ) engine=InnoDB;

CREATE  TABLE skyvault.nationalities ( 
	id                   INT    NOT NULL AUTO_INCREMENT  PRIMARY KEY,
	nationality_name     VARCHAR(100)    NOT NULL   
 ) engine=InnoDB;

ALTER TABLE skyvault.customer_profiles ADD CONSTRAINT fk_customer_profiles_communication_methods FOREIGN KEY ( preferred_comm_id ) REFERENCES skyvault.communication_methods( id ) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE skyvault.visas ADD CONSTRAINT fk_visas_countries FOREIGN KEY ( country_id ) REFERENCES skyvault.countries( id ) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE skyvault.customer_profiles ADD CONSTRAINT fk_customer_profiles_customer_profiles FOREIGN KEY ( parent_id ) REFERENCES skyvault.customer_profiles( id ) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE skyvault.frequent_flyer_numbers ADD CONSTRAINT fk_frequent_flyer_numbers_customer_profiles FOREIGN KEY ( customer_profile_id ) REFERENCES skyvault.customer_profiles( id ) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE skyvault.jobs ADD CONSTRAINT fk_jobs_customer_profiles FOREIGN KEY ( customer_profile_id ) REFERENCES skyvault.customer_profiles( id ) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE skyvault.passports ADD CONSTRAINT fk_passports_nationalities FOREIGN KEY ( nationality_id ) REFERENCES skyvault.nationalities( id ) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE skyvault.jobs ADD CONSTRAINT fk_jobs_notification_templates FOREIGN KEY ( template_id ) REFERENCES skyvault.notification_templates( id ) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE skyvault.notification_templates ADD CONSTRAINT fk_notification_templates_notification_types FOREIGN KEY ( notification_type ) REFERENCES skyvault.notification_types( id ) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE skyvault.passports ADD CONSTRAINT fk_passports_customer_profiles FOREIGN KEY ( customer_profile_id ) REFERENCES skyvault.customer_profiles( id ) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE skyvault.customer_profiles ADD CONSTRAINT fk_customer_profiles_salutations FOREIGN KEY ( salutation_id ) REFERENCES skyvault.salutations( id ) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE skyvault.customer_profiles ADD CONSTRAINT fk_customer_profiles_system_users FOREIGN KEY ( system_user_id ) REFERENCES skyvault.system_users( id ) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE skyvault.visas ADD CONSTRAINT fk_visas_passports FOREIGN KEY ( passport_id ) REFERENCES skyvault.passports( id ) ON DELETE RESTRICT ON UPDATE RESTRICT;
