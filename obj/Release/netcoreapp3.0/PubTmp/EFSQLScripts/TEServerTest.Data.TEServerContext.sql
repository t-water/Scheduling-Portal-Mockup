IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191201001811_initialMigration')
BEGIN
    CREATE TABLE [Venues] (
        [ID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbreviation] nvarchar(max) NULL,
        CONSTRAINT [PK_Venues] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191201001811_initialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191201001811_initialMigration', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191201005225_changedDatabaseName')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Venues]') AND [c].[name] = N'Name');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Venues] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Venues] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191201005225_changedDatabaseName')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Venues]') AND [c].[name] = N'Abbreviation');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Venues] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Venues] ALTER COLUMN [Abbreviation] nvarchar(5) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191201005225_changedDatabaseName')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191201005225_changedDatabaseName', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191202185515_ShiftsTableCreated')
BEGIN
    CREATE TABLE [Shifts] (
        [ID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Start] datetime2 NOT NULL,
        [End] datetime2 NOT NULL,
        [VenueID] int NOT NULL,
        CONSTRAINT [PK_Shifts] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Shifts_Venues_VenueID] FOREIGN KEY ([VenueID]) REFERENCES [Venues] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191202185515_ShiftsTableCreated')
BEGIN
    CREATE INDEX [IX_Shifts_VenueID] ON [Shifts] ([VenueID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191202185515_ShiftsTableCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191202185515_ShiftsTableCreated', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191203155229_ConfigureIdentity')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191203155229_ConfigureIdentity', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191204171959_AddedApplicationUserClass')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Birthday] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191204171959_AddedApplicationUserClass')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [FirstName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191204171959_AddedApplicationUserClass')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [LastName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191204171959_AddedApplicationUserClass')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [WorkStudyAllowance] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191204171959_AddedApplicationUserClass')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191204171959_AddedApplicationUserClass', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191205175749_AddedUsersShiftsJoin')
BEGIN
    CREATE TABLE [UsersShifts] (
        [UserShiftID] int NOT NULL IDENTITY,
        [UserID] nvarchar(450) NULL,
        [ShiftID] int NOT NULL,
        [UserStart] datetime2 NOT NULL,
        [UserEnd] datetime2 NOT NULL,
        CONSTRAINT [PK_UsersShifts] PRIMARY KEY ([UserShiftID]),
        CONSTRAINT [FK_UsersShifts_Shifts_ShiftID] FOREIGN KEY ([ShiftID]) REFERENCES [Shifts] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_UsersShifts_AspNetUsers_UserID] FOREIGN KEY ([UserID]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191205175749_AddedUsersShiftsJoin')
BEGIN
    CREATE INDEX [IX_UsersShifts_ShiftID] ON [UsersShifts] ([ShiftID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191205175749_AddedUsersShiftsJoin')
BEGIN
    CREATE INDEX [IX_UsersShifts_UserID] ON [UsersShifts] ([UserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191205175749_AddedUsersShiftsJoin')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191205175749_AddedUsersShiftsJoin', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191206180136_AddedPositionToUsersShifts')
BEGIN
    ALTER TABLE [UsersShifts] ADD [PositionID] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191206180136_AddedPositionToUsersShifts')
BEGIN
    CREATE INDEX [IX_UsersShifts_PositionID] ON [UsersShifts] ([PositionID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191206180136_AddedPositionToUsersShifts')
BEGIN
    ALTER TABLE [UsersShifts] ADD CONSTRAINT [FK_UsersShifts_AspNetRoles_PositionID] FOREIGN KEY ([PositionID]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191206180136_AddedPositionToUsersShifts')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191206180136_AddedPositionToUsersShifts', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208001815_AddedTimeOff')
BEGIN
    ALTER TABLE [UsersShifts] DROP CONSTRAINT [FK_UsersShifts_AspNetUsers_UserID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208001815_AddedTimeOff')
BEGIN
    DROP INDEX [IX_UsersShifts_UserID] ON [UsersShifts];
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UsersShifts]') AND [c].[name] = N'UserID');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [UsersShifts] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [UsersShifts] ALTER COLUMN [UserID] nvarchar(450) NOT NULL;
    CREATE INDEX [IX_UsersShifts_UserID] ON [UsersShifts] ([UserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208001815_AddedTimeOff')
BEGIN
    CREATE TABLE [TimeOff] (
        [TimeOffID] int NOT NULL IDENTITY,
        [Start] datetime2 NOT NULL,
        [NumberOfDays] int NOT NULL,
        [AdditionalInformation] nvarchar(500) NULL,
        [UserID] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_TimeOff] PRIMARY KEY ([TimeOffID]),
        CONSTRAINT [FK_TimeOff_AspNetUsers_UserID] FOREIGN KEY ([UserID]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208001815_AddedTimeOff')
BEGIN
    CREATE INDEX [IX_TimeOff_UserID] ON [TimeOff] ([UserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208001815_AddedTimeOff')
BEGIN
    ALTER TABLE [UsersShifts] ADD CONSTRAINT [FK_UsersShifts_AspNetUsers_UserID] FOREIGN KEY ([UserID]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208001815_AddedTimeOff')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191208001815_AddedTimeOff', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208175918_AddedStatusToTimeOff')
BEGIN
    ALTER TABLE [TimeOff] ADD [RequestStatus] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208175918_AddedStatusToTimeOff')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191208175918_AddedStatusToTimeOff', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208200814_AddedShiftSwaps')
BEGIN
    CREATE TABLE [ShiftSwaps] (
        [ShiftSwapID] int NOT NULL IDENTITY,
        [UserShiftID] int NOT NULL,
        [TakenByID] nvarchar(450) NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_ShiftSwaps] PRIMARY KEY ([ShiftSwapID]),
        CONSTRAINT [FK_ShiftSwaps_AspNetUsers_TakenByID] FOREIGN KEY ([TakenByID]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ShiftSwaps_UsersShifts_UserShiftID] FOREIGN KEY ([UserShiftID]) REFERENCES [UsersShifts] ([UserShiftID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208200814_AddedShiftSwaps')
BEGIN
    CREATE INDEX [IX_ShiftSwaps_TakenByID] ON [ShiftSwaps] ([TakenByID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208200814_AddedShiftSwaps')
BEGIN
    CREATE INDEX [IX_ShiftSwaps_UserShiftID] ON [ShiftSwaps] ([UserShiftID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208200814_AddedShiftSwaps')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191208200814_AddedShiftSwaps', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208202206_ChangedStatusColumnNameShiftSwaps')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShiftSwaps]') AND [c].[name] = N'Status');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ShiftSwaps] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [ShiftSwaps] DROP COLUMN [Status];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208202206_ChangedStatusColumnNameShiftSwaps')
BEGIN
    ALTER TABLE [ShiftSwaps] ADD [RequestStatus] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208202206_ChangedStatusColumnNameShiftSwaps')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191208202206_ChangedStatusColumnNameShiftSwaps', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208214301_MergedUserShiftsAndShiftSwaps')
BEGIN
    DROP TABLE [ShiftSwaps];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208214301_MergedUserShiftsAndShiftSwaps')
BEGIN
    ALTER TABLE [UsersShifts] ADD [RequestStatus] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208214301_MergedUserShiftsAndShiftSwaps')
BEGIN
    ALTER TABLE [UsersShifts] ADD [TakenByID] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208214301_MergedUserShiftsAndShiftSwaps')
BEGIN
    CREATE INDEX [IX_UsersShifts_TakenByID] ON [UsersShifts] ([TakenByID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208214301_MergedUserShiftsAndShiftSwaps')
BEGIN
    ALTER TABLE [UsersShifts] ADD CONSTRAINT [FK_UsersShifts_AspNetUsers_TakenByID] FOREIGN KEY ([TakenByID]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191208214301_MergedUserShiftsAndShiftSwaps')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191208214301_MergedUserShiftsAndShiftSwaps', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191209194445_AddedReasonForDenialToTimeOff')
BEGIN
    ALTER TABLE [TimeOff] ADD [ReasonForDenial] nvarchar(500) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191209194445_AddedReasonForDenialToTimeOff')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191209194445_AddedReasonForDenialToTimeOff', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191210194829_AddedAvailability')
BEGIN
    CREATE TABLE [Availabilities] (
        [AvailabilityID] int NOT NULL IDENTITY,
        [Day] int NOT NULL,
        [Start] datetime2 NOT NULL,
        [End] datetime2 NOT NULL,
        [IsSelected] bit NOT NULL,
        [UserID] nvarchar(450) NULL,
        CONSTRAINT [PK_Availabilities] PRIMARY KEY ([AvailabilityID]),
        CONSTRAINT [FK_Availabilities_AspNetUsers_UserID] FOREIGN KEY ([UserID]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191210194829_AddedAvailability')
BEGIN
    CREATE INDEX [IX_Availabilities_UserID] ON [Availabilities] ([UserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191210194829_AddedAvailability')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191210194829_AddedAvailability', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191212022525_AddedDefaultPositionCountDictionaryToVenue')
BEGIN
    ALTER TABLE [Venues] ADD [DefaultCommunityRoomLiaisons] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191212022525_AddedDefaultPositionCountDictionaryToVenue')
BEGIN
    ALTER TABLE [Venues] ADD [DefaultConcessionsManagers] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191212022525_AddedDefaultPositionCountDictionaryToVenue')
BEGIN
    ALTER TABLE [Venues] ADD [DefaultEventUshers] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191212022525_AddedDefaultPositionCountDictionaryToVenue')
BEGIN
    ALTER TABLE [Venues] ADD [DefaultExecutiveHouseManagers] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191212022525_AddedDefaultPositionCountDictionaryToVenue')
BEGIN
    ALTER TABLE [Venues] ADD [DefaultFloorManagers] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191212022525_AddedDefaultPositionCountDictionaryToVenue')
BEGIN
    ALTER TABLE [Venues] ADD [DefaultHouseManagers] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191212022525_AddedDefaultPositionCountDictionaryToVenue')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191212022525_AddedDefaultPositionCountDictionaryToVenue', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191214200519_AddedEndDateToTimeOff')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TimeOff]') AND [c].[name] = N'NumberOfDays');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [TimeOff] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [TimeOff] DROP COLUMN [NumberOfDays];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191214200519_AddedEndDateToTimeOff')
BEGIN
    ALTER TABLE [TimeOff] ADD [End] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191214200519_AddedEndDateToTimeOff')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191214200519_AddedEndDateToTimeOff', N'3.1.1');
END;

GO

