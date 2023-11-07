CREATE TABLE "PolizasCoexpo"(
    "id" BIGINT NOT NULL,
    "polizaNo" BIGINT NOT NULL,
    "countryID" BIGINT NOT NULL,
    "amount" DECIMAL(8, 2) NOT NULL,
    "coexpoID" BIGINT NOT NULL
);
ALTER TABLE
    "PolizasCoexpo" ADD CONSTRAINT "polizascoexpo_id_primary" PRIMARY KEY("id");
CREATE UNIQUE INDEX "polizascoexpo_polizano_unique" ON
    "PolizasCoexpo"("polizaNo");
CREATE TABLE "Aduanas"(
    "id" BIGINT NOT NULL,
    "aduanasID" BIGINT NOT NULL,
    "nombreAduana" BIGINT NOT NULL,
    "abreviacionAduana" BIGINT NOT NULL
);
ALTER TABLE
    "Aduanas" ADD CONSTRAINT "aduanas_id_primary" PRIMARY KEY("id");
CREATE UNIQUE INDEX "aduanas_aduanasid_unique" ON
    "Aduanas"("aduanasID");
CREATE TABLE "ExportHeader"(
    "id" BIGINT NOT NULL,
    "InvoiceNo" BIGINT NOT NULL,
    "invoiceDate" DATE NOT NULL,
    "serialNo" VARCHAR(255) NULL,
    "consignatarioID" BIGINT NOT NULL,
    "aduanasID" BIGINT NOT NULL,
    "countryID" BIGINT NOT NULL,
    "cargadorID" BIGINT NOT NULL,
    "boardingDate" DATE NOT NULL,
    "exchangeRate" DECIMAL(8, 2) NOT NULL,
    "description" NVARCHAR(255) NOT NULL,
    "docTypeID" BIGINT NOT NULL,
    "duaSimplificada" INT NULL,
    "complementaria" NVARCHAR(255) NULL,
    "incotermID" INT NOT NULL,
    "closed" BIT NOT NULL,
    "handlerID" BIGINT NULL,
    "supervisorID" BIGINT NULL,
    "anulledDate" BIGINT NULL,
    "primaImportada" BIT NOT NULL
);
ALTER TABLE
    "ExportHeader" ADD CONSTRAINT "exportheader_id_primary" PRIMARY KEY("id");
CREATE UNIQUE INDEX "exportheader_invoiceno_unique" ON
    "ExportHeader"("InvoiceNo");
CREATE TABLE "Country"(
    "id" BIGINT NOT NULL,
    "countryID" BIGINT NOT NULL,
    "countryName" BIGINT NOT NULL,
    "countryISO" BIGINT NOT NULL
);
ALTER TABLE
    "Country" ADD CONSTRAINT "country_id_primary" PRIMARY KEY("id");
CREATE UNIQUE INDEX "country_countryid_unique" ON
    "Country"("countryID");
CREATE TABLE "PolizaImportacion"(
    "ID" BIGINT NOT NULL,
    "invoiceNo" BIGINT NOT NULL,
    "poliza" BIGINT NOT NULL,
    "countryID" BIGINT NOT NULL,
    "quantity" DECIMAL(8, 2) NOT NULL,
    "amount" DECIMAL(8, 2) NOT NULL,
    "imp" BIGINT NOT NULL,
    "linea" BIGINT NOT NULL,
    "date" DATETIME NOT NULL
);
ALTER TABLE
    "PolizaImportacion" ADD CONSTRAINT "polizaimportacion_id_primary" PRIMARY KEY("ID");
CREATE TABLE "Consignatarios"(
    "id" BIGINT NOT NULL,
    "consignatarioID" BIGINT NOT NULL,
    "nombreConsignatario" BIGINT NOT NULL
);
ALTER TABLE
    "Consignatarios" ADD CONSTRAINT "consignatarios_id_primary" PRIMARY KEY("id");
CREATE UNIQUE INDEX "consignatarios_consignatarioid_unique" ON
    "Consignatarios"("consignatarioID");
CREATE TABLE "Cargador"(
    "id" BIGINT NOT NULL,
    "cargadorID" BIGINT NOT NULL,
    "descripcion" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Cargador" ADD CONSTRAINT "cargador_id_primary" PRIMARY KEY("id");
CREATE UNIQUE INDEX "cargador_cargadorid_unique" ON
    "Cargador"("cargadorID");
CREATE TABLE "FacturaCoexpo"(
    "ID" BIGINT NOT NULL,
    "invoiceNo" BIGINT NOT NULL,
    "coexpoID" BIGINT NOT NULL,
    "proveedor" NVARCHAR(255) NOT NULL,
    "tipoProveedor" BIGINT NOT NULL,
    "factura" NVARCHAR(255) NOT NULL,
    "amount" DECIMAL(8, 2) NOT NULL
);
ALTER TABLE
    "FacturaCoexpo" ADD CONSTRAINT "facturacoexpo_id_primary" PRIMARY KEY("ID");
CREATE UNIQUE INDEX "facturacoexpo_coexpoid_unique" ON
    "FacturaCoexpo"("coexpoID");
CREATE UNIQUE INDEX "facturacoexpo_factura_unique" ON
    "FacturaCoexpo"("factura");
CREATE TABLE "DetalleProducto"(
    "ID" BIGINT NOT NULL,
    "invoiceNo" BIGINT NOT NULL,
    "codigoHTS" NVARCHAR(255) NOT NULL,
    "description" NVARCHAR(255) NOT NULL,
    "categoria" NVARCHAR(255) NOT NULL,
    "cantidad" BIGINT NOT NULL,
    "medida" BIGINT NOT NULL,
    "pricePerUnit" BIGINT NOT NULL
);
ALTER TABLE
    "DetalleProducto" ADD CONSTRAINT "detalleproducto_id_primary" PRIMARY KEY("ID");
CREATE UNIQUE INDEX "detalleproducto_invoiceno_unique" ON
    "DetalleProducto"("invoiceNo");
ALTER TABLE
    "ExportHeader" ADD CONSTRAINT "exportheader_consignatarioid_foreign" FOREIGN KEY("consignatarioID") REFERENCES "Consignatarios"("consignatarioID");
ALTER TABLE
    "ExportHeader" ADD CONSTRAINT "exportheader_countryid_foreign" FOREIGN KEY("countryID") REFERENCES "Country"("countryID");
ALTER TABLE
    "ExportHeader" ADD CONSTRAINT "exportheader_aduanasid_foreign" FOREIGN KEY("aduanasID") REFERENCES "Aduanas"("aduanasID");
ALTER TABLE
    "PolizasCoexpo" ADD CONSTRAINT "polizascoexpo_coexpoid_foreign" FOREIGN KEY("coexpoID") REFERENCES "FacturaCoexpo"("coexpoID");
ALTER TABLE
    "PolizasCoexpo" ADD CONSTRAINT "polizascoexpo_countryid_foreign" FOREIGN KEY("countryID") REFERENCES "Country"("countryID");
ALTER TABLE
    "PolizaImportacion" ADD CONSTRAINT "polizaimportacion_invoiceno_foreign" FOREIGN KEY("invoiceNo") REFERENCES "ExportHeader"("InvoiceNo");
ALTER TABLE
    "FacturaCoexpo" ADD CONSTRAINT "facturacoexpo_invoiceno_foreign" FOREIGN KEY("invoiceNo") REFERENCES "ExportHeader"("InvoiceNo");
ALTER TABLE
    "ExportHeader" ADD CONSTRAINT "exportheader_cargadorid_foreign" FOREIGN KEY("cargadorID") REFERENCES "Cargador"("cargadorID");
ALTER TABLE
    "PolizaImportacion" ADD CONSTRAINT "polizaimportacion_countryid_foreign" FOREIGN KEY("countryID") REFERENCES "Country"("countryID");
ALTER TABLE
    "DetalleProducto" ADD CONSTRAINT "detalleproducto_invoiceno_foreign" FOREIGN KEY("invoiceNo") REFERENCES "ExportHeader"("InvoiceNo");