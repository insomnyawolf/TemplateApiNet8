BEGIN TRANSACTION;
DROP TABLE IF EXISTS "Show";
CREATE TABLE IF NOT EXISTS "Show" (
	"Id"	GUID NOT NULL UNIQUE,
	"Url"	TEXT,
	"Name"	TEXT,
	"Runtime"	INTEGER,
	"AverageRuntime"	INTEGER,
	"Premiered"	TEXT,
	"Ended"	TEXT,
	"OfficialSite"	TEXT,
	"Weight"	INTEGER,
	"Summary"	TEXT,
	"Updated"	INTEGER,
	PRIMARY KEY("Id")
);
DROP TABLE IF EXISTS "Kind";
CREATE TABLE IF NOT EXISTS "Kind" (
	"Id"	GUID NOT NULL UNIQUE,
	"Name"	TEXT,
	PRIMARY KEY("Id")
);
DROP TABLE IF EXISTS "Status";
CREATE TABLE IF NOT EXISTS "Status" (
	"Id"	GUID NOT NULL UNIQUE,
	"Name"	TEXT,
	PRIMARY KEY("Id")
);
DROP TABLE IF EXISTS "Language";
CREATE TABLE IF NOT EXISTS "Language" (
	"Id"	GUID NOT NULL UNIQUE,
	"Name"	TEXT,
	PRIMARY KEY("Id")
);
DROP TABLE IF EXISTS "Network";
CREATE TABLE IF NOT EXISTS "Network" (
	"Id"	GUID NOT NULL UNIQUE,
	"Name"	TEXT,
	"OfficialSite"	TEXT,
	PRIMARY KEY("Id")
);
DROP TABLE IF EXISTS "Country";
CREATE TABLE IF NOT EXISTS "Country" (
	"Id"	GUID NOT NULL UNIQUE,
	"Name"	TEXT,
	"Code"	TEXT,
	"Timezone"	TEXT,
	PRIMARY KEY("Id")
);
DROP TABLE IF EXISTS "External";
CREATE TABLE IF NOT EXISTS "External" (
	"Id"	GUID NOT NULL UNIQUE,
	"Name"	TEXT,
	PRIMARY KEY("Id")
);
DROP TABLE IF EXISTS "Image";
CREATE TABLE IF NOT EXISTS "Image" (
	"Id"	GUID NOT NULL UNIQUE,
	"ShowId"	GUID NOT NULL,
	"Medium"	TEXT,
	"Original"	TEXT,
	PRIMARY KEY("Id")
);
DROP TABLE IF EXISTS "Episode";
CREATE TABLE IF NOT EXISTS "Episode" (
	"Id"	GUID NOT NULL UNIQUE,
	"ShowId"	GUID NOT NULL,
	"Href"	TEXT,
	PRIMARY KEY("Id")
);
DROP TABLE IF EXISTS "Links";
CREATE TABLE IF NOT EXISTS "Links" (
	"Id"	GUID NOT NULL UNIQUE,
	"ShowId"	GUID NOT NULL,
	"SelfId"	GUID,
	"PreviousId"	GUID,
	PRIMARY KEY("Id"),
	FOREIGN KEY("SelfId") REFERENCES "Episode"("Id"),
	FOREIGN KEY("PreviousId") REFERENCES "Episode"("Id")
);
DROP TABLE IF EXISTS "Genere";
CREATE TABLE IF NOT EXISTS "Genere" (
	"Id"	GUID NOT NULL UNIQUE,
	"Name"	TEXT NOT NULL,
	PRIMARY KEY("Id")
);
DROP TABLE IF EXISTS "Schedule";
CREATE TABLE IF NOT EXISTS "Schedule" (
	"Id"	GUID NOT NULL UNIQUE,
	"ShowId"	GUID NOT NULL,
	"Time"	TEXT,
	PRIMARY KEY("Id"),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id")
);
DROP TABLE IF EXISTS "Day";
CREATE TABLE IF NOT EXISTS "Day" (
	"Id"	GUID NOT NULL UNIQUE,
	"Name"	TEXT NOT NULL,
	PRIMARY KEY("Id")
);
DROP TABLE IF EXISTS "Rating";
CREATE TABLE IF NOT EXISTS "Rating" (
	"Id"	GUID NOT NULL UNIQUE,
	"ShowId"	GUID NOT NULL,
	"Average"	REAL NOT NULL,
	PRIMARY KEY("Id"),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id")
);
DROP TABLE IF EXISTS "ShowExternal";
CREATE TABLE IF NOT EXISTS "ShowExternal" (
	"Id"	GUID NOT NULL UNIQUE,
	"ShowId"	GUID NOT NULL,
	"ExternalId"	GUID NOT NULL,
	"Value"	REAL,
	PRIMARY KEY("Id"),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id"),
	FOREIGN KEY("ExternalId") REFERENCES "External"("Id")
);
DROP TABLE IF EXISTS "ScheduleDay";
CREATE TABLE IF NOT EXISTS "ScheduleDay" (
	"Id"	GUID NOT NULL UNIQUE,
	"ScheduleId"	GUID NOT NULL,
	"DayId"	GUID NOT NULL,
	PRIMARY KEY("Id"),
	FOREIGN KEY("ScheduleId") REFERENCES "Schedule"("Id"),
	FOREIGN KEY("DayId") REFERENCES "Day"("Id")
);
DROP TABLE IF EXISTS "ShowGenere";
CREATE TABLE IF NOT EXISTS "ShowGenere" (
	"Id"	GUID NOT NULL UNIQUE,
	"ShowId"	GUID NOT NULL,
	"GenereId"	GUID NOT NULL,
	PRIMARY KEY("Id"),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id"),
	FOREIGN KEY("GenereId") REFERENCES "Genere"("Id")
);
DROP TABLE IF EXISTS "ShowNetwork";
CREATE TABLE IF NOT EXISTS "ShowNetwork" (
	"Id"	GUID NOT NULL UNIQUE,
	"ShowId"	GUID NOT NULL,
	"NetworkId"	GUID NOT NULL,
	PRIMARY KEY("Id"),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id"),
	FOREIGN KEY("NetworkId") REFERENCES "Network"("Id")
);
DROP TABLE IF EXISTS "CountryNetwork";
CREATE TABLE IF NOT EXISTS "CountryNetwork" (
	"Id"	GUID NOT NULL UNIQUE,
	"NetworkId"	GUID NOT NULL,
	"CountryId"	GUID NOT NULL,
	PRIMARY KEY("Id"),
	FOREIGN KEY("NetworkId") REFERENCES "Network"("Id"),
	FOREIGN KEY("CountryId") REFERENCES "Country"("Id")
);
DROP TABLE IF EXISTS "ShowLanguage";
CREATE TABLE IF NOT EXISTS "ShowLanguage" (
	"Id"	GUID NOT NULL UNIQUE,
	"ShowId"	GUID NOT NULL,
	"LanguageId"	GUID NOT NULL,
	PRIMARY KEY("Id"),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id"),
	FOREIGN KEY("LanguageId") REFERENCES "Language"("Id")
);
DROP TABLE IF EXISTS "ShowKind";
CREATE TABLE IF NOT EXISTS "ShowKind" (
	"Id"	GUID NOT NULL UNIQUE,
	"ShowId"	GUID NOT NULL,
	"KindId"	GUID NOT NULL,
	PRIMARY KEY("Id"),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id"),
	FOREIGN KEY("KindId") REFERENCES "Kind"("Id")
);
DROP TABLE IF EXISTS "ShowStatus";
CREATE TABLE IF NOT EXISTS "ShowStatus" (
	"Id"	GUID NOT NULL UNIQUE,
	"ShowId"	GUID NOT NULL,
	"StatusId"	GUID NOT NULL,
	PRIMARY KEY("Id"),
	FOREIGN KEY("ShowId") REFERENCES "Show"("Id"),
	FOREIGN KEY("StatusId") REFERENCES "Status"("Id")
);
COMMIT;
