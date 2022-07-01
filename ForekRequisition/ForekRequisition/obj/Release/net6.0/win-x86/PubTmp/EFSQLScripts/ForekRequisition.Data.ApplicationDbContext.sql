IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227112933_InitialMigration')
BEGIN
    CREATE TABLE [Applicants] (
        [Id] uniqueidentifier NOT NULL,
        [Trade] int NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [Province] int NOT NULL,
        [Initials] int NOT NULL,
        [IDNumber] nvarchar(13) NOT NULL,
        [Race] int NOT NULL,
        [Gender] int NOT NULL,
        [Disability] nvarchar(max) NULL,
        [DisabilityAttachment] nvarchar(max) NULL,
        [Municipality] int NOT NULL,
        [AddressIsSameAsPostal] bit NOT NULL,
        [AddressLine1] nvarchar(max) NOT NULL,
        [City] nvarchar(max) NULL,
        [Code] nvarchar(max) NOT NULL,
        [HomeTel] nvarchar(max) NULL,
        [Cellphone] nvarchar(10) NOT NULL,
        [ReferenceNumber] nvarchar(max) NULL,
        [Email] nvarchar(max) NOT NULL,
        [AlternativeContacPers] nvarchar(max) NULL,
        [AlternativeNum] nvarchar(10) NULL,
        [ProspectiveEmployer] nvarchar(max) NULL,
        [IsCurrentlyEmployed] bit NOT NULL,
        [HasSignedTerms] bit NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedOn] datetime2 NULL,
        [UpdatedOn] datetime2 NULL,
        [CreatedBy] nvarchar(max) NULL,
        [UpdatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_Applicants] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227112933_InitialMigration')
BEGIN
    CREATE TABLE [Education] (
        [Id] uniqueidentifier NOT NULL,
        [NameofHSchool] nvarchar(max) NOT NULL,
        [Town] nvarchar(max) NOT NULL,
        [Municipality] int NOT NULL,
        [From] datetime2 NULL,
        [Till] datetime2 NULL,
        [AppStatus] int NOT NULL,
        [StatusDescription] nvarchar(max) NULL,
        [HighestQualDoc] nvarchar(max) NULL,
        [IdentityPDF] nvarchar(max) NULL,
        [ApprenticeCV] nvarchar(max) NULL,
        [MatricResults] nvarchar(max) NULL,
        [BankingDocs] nvarchar(max) NULL,
        [ProofRes] nvarchar(max) NULL,
        [AffidavitInSupport] nvarchar(max) NULL,
        [HostEmpNot] nvarchar(max) NULL,
        [GradePassed] int NOT NULL,
        [SubjectOne] int NOT NULL,
        [ApplicantId] uniqueidentifier NOT NULL,
        [QualificationName] nvarchar(max) NULL,
        [QualificationType] int NULL,
        [QualificationLevel] int NULL,
        [HasPassedSubjects] int NULL,
        [IsFrom] datetime2 NULL,
        [FETName] nvarchar(max) NULL,
        [IsTill] datetime2 NULL,
        [MathSubject] nvarchar(max) NOT NULL,
        [ScienceSubject] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedOn] datetime2 NULL,
        [UpdatedOn] datetime2 NULL,
        [CreatedBy] nvarchar(max) NULL,
        [UpdatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_Education] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227112933_InitialMigration')
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [ConfirmPassword] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Phone] nvarchar(10) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [IsEmailVerified] bit NOT NULL,
        [ActivationCode] uniqueidentifier NOT NULL,
        [ResetPasswordCode] nvarchar(max) NULL,
        [LastLoginDate] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [CreatedOn] datetime2 NULL,
        [UpdatedOn] datetime2 NULL,
        [CreatedBy] nvarchar(max) NULL,
        [UpdatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227112933_InitialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227112933_InitialMigration', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302075301_addNewModels')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Education]') AND [c].[name] = N'AppStatus');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Education] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Education] DROP COLUMN [AppStatus];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302075301_addNewModels')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Education]') AND [c].[name] = N'StatusDescription');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Education] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Education] DROP COLUMN [StatusDescription];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302075301_addNewModels')
BEGIN
    ALTER TABLE [Users] ADD [Role] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302075301_addNewModels')
BEGIN
    CREATE TABLE [ApprenticeStatus] (
        [Id] uniqueidentifier NOT NULL,
        [ApplicationStatus] int NOT NULL,
        [Description] nvarchar(max) NULL,
        [ApplicantId] uniqueidentifier NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedOn] datetime2 NULL,
        [UpdatedOn] datetime2 NULL,
        [CreatedBy] nvarchar(max) NULL,
        [UpdatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_ApprenticeStatus] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ApprenticeStatus_Applicants_ApplicantId] FOREIGN KEY ([ApplicantId]) REFERENCES [Applicants] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302075301_addNewModels')
BEGIN
    CREATE INDEX [IX_ApprenticeStatus_ApplicantId] ON [ApprenticeStatus] ([ApplicantId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302075301_addNewModels')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220302075301_addNewModels', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310123620_AddMedForm')
BEGIN
    ALTER TABLE [Education] ADD [MedicalCert] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310123620_AddMedForm')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220310123620_AddMedForm', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315104930_UpdateApplicantModel')
BEGIN
    ALTER TABLE [Applicants] ADD [ProgrammeType] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315104930_UpdateApplicantModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220315104930_UpdateApplicantModel', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320135110_TrackApplicationStatus')
BEGIN
    ALTER TABLE [Education] ADD [IsApplicationComplete] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320135110_TrackApplicationStatus')
BEGIN
    ALTER TABLE [Education] ADD [IsIncomplete] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320135110_TrackApplicationStatus')
BEGIN
    ALTER TABLE [Education] ADD [IsPartiallyComplete] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320135110_TrackApplicationStatus')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220320135110_TrackApplicationStatus', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220411124356_AddSelectionTestRes')
BEGIN
    CREATE TABLE [ApprenticeResults] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [IDNumber] nvarchar(13) NOT NULL,
        [PercentageObtained] float NOT NULL,
        [Cellphone] nvarchar(13) NOT NULL,
        [DoesApplicantQualify] int NULL,
        [ProgrammeAppliedFor] int NULL,
        [ProgrammeType] int NULL,
        [IsActive] bit NOT NULL,
        [CreatedOn] datetime2 NULL,
        [UpdatedOn] datetime2 NULL,
        [CreatedBy] nvarchar(max) NULL,
        [UpdatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_ApprenticeResults] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220411124356_AddSelectionTestRes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220411124356_AddSelectionTestRes', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220419135806_addResults')
BEGIN
    DROP TABLE [ApprenticeResults];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220419135806_addResults')
BEGIN
    CREATE TABLE [AppResults] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [LastName] nvarchar(20) NOT NULL,
        [IdNumber] nvarchar(13) NOT NULL,
        [Cellphone] nvarchar(10) NULL,
        [DoesApplicantQualify] int NOT NULL,
        [Programme] int NOT NULL,
        [ProgrammeType] int NOT NULL,
        [Recommendation] nvarchar(max) NULL,
        [PercentageObtained] float NOT NULL,
        [AddMore] bit NOT NULL,
        [District] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedOn] datetime2 NULL,
        [UpdatedOn] datetime2 NULL,
        [CreatedBy] nvarchar(max) NULL,
        [UpdatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_AppResults] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220419135806_addResults')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220419135806_addResults', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220419163135_AddGender')
BEGIN
    ALTER TABLE [AppResults] ADD [Gender] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220419163135_AddGender')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220419163135_AddGender', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220425110915_AddDistrictMun')
BEGIN
    ALTER TABLE [AppResults] ADD [MunicipalDistrict] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220425110915_AddDistrictMun')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220425110915_AddDistrictMun', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220510123720_AddApproved')
BEGIN
    ALTER TABLE [AppResults] ADD [IsApproved] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220510123720_AddApproved')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220510123720_AddApproved', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512101237_IsApprovedAddition')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220512101237_IsApprovedAddition', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220516115006_SignedOffer')
BEGIN
    ALTER TABLE [Applicants] ADD [HasAcceptedOffer] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220516115006_SignedOffer')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220516115006_SignedOffer', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524122228_studentEntityMigration')
BEGIN
    CREATE TABLE [Students] (
        [Id] uniqueidentifier NOT NULL,
        [Surname] nvarchar(20) NOT NULL,
        [FullNames] nvarchar(20) NOT NULL,
        [IDNumber] nvarchar(13) NOT NULL,
        [AddressType] int NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [Code] nvarchar(max) NOT NULL,
        [Telephone] nvarchar(max) NULL,
        [Cellphone] nvarchar(max) NULL,
        [GradePassed] int NOT NULL,
        [HighestQual] int NOT NULL,
        [GuardianName] nvarchar(max) NULL,
        [GuardianLastName] nvarchar(max) NOT NULL,
        [GuardianCell] nvarchar(10) NOT NULL,
        [GuardianTel] nvarchar(max) NULL,
        [Relationship] int NOT NULL,
        [Course] int NOT NULL,
        [CourseDescription] nvarchar(max) NULL,
        [HasFunding] int NOT NULL,
        [FunderNameSurname] nvarchar(max) NULL,
        [FunderContact] nvarchar(max) NULL,
        [RSAIDAttach] nvarchar(max) NULL,
        [IsEmployed] bit NOT NULL,
        CONSTRAINT [PK_Students] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524122228_studentEntityMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220524122228_studentEntityMigration', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525110522_ModifyStudent2')
BEGIN
    ALTER TABLE [Students] ADD [Nationality] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525110522_ModifyStudent2')
BEGIN
    ALTER TABLE [Students] ADD [StudyMode] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525110522_ModifyStudent2')
BEGIN
    ALTER TABLE [Students] ADD [Title] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525110522_ModifyStudent2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220525110522_ModifyStudent2', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220531071836_addAdditionalUserAttr')
BEGIN
    ALTER TABLE [Students] ADD [HighestQualCertAttach] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220531071836_addAdditionalUserAttr')
BEGIN
    ALTER TABLE [Students] ADD [ProofofRes] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220531071836_addAdditionalUserAttr')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220531071836_addAdditionalUserAttr', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607114558_Offer')
BEGIN
    CREATE TABLE [ApplicantOffer] (
        [Id] uniqueidentifier NOT NULL,
        [ApplicantId] uniqueidentifier NOT NULL,
        [HasAcceptedPlumbingOffer] bit NOT NULL,
        [HasAcceptedWeldingOffer] bit NOT NULL,
        [HasSignedOffer] bit NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedOn] datetime2 NULL,
        [UpdatedOn] datetime2 NULL,
        [CreatedBy] nvarchar(max) NULL,
        [UpdatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_ApplicantOffer] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220607114558_Offer')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220607114558_Offer', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220614084512_modifyStudent')
BEGIN
    EXEC sp_rename N'[Students].[ProofofRes]', N'ProofOfRes', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220614084512_modifyStudent')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'ProofOfRes');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Students] ALTER COLUMN [ProofOfRes] nvarchar(max) NOT NULL;
    ALTER TABLE [Students] ADD DEFAULT N'' FOR [ProofOfRes];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220614084512_modifyStudent')
BEGIN
    ALTER TABLE [Students] ADD [Email] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220614084512_modifyStudent')
BEGIN
    ALTER TABLE [Students] ADD [GuadianId] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220614084512_modifyStudent')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220614084512_modifyStudent', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220614090232_AddContactDetails')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'Cellphone');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Students] ALTER COLUMN [Cellphone] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220614090232_AddContactDetails')
BEGIN
    ALTER TABLE [Students] ADD [AlternativeNum] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220614090232_AddContactDetails')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220614090232_AddContactDetails', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220614120139_ModifyUser')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'ProofOfRes');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Students] ALTER COLUMN [ProofOfRes] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220614120139_ModifyUser')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'GuadianId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Students] ALTER COLUMN [GuadianId] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220614120139_ModifyUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220614120139_ModifyUser', N'6.0.5');
END;
GO

COMMIT;
GO

