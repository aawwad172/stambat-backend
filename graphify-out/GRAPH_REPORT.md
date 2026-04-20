# Graph Report - /Users/aawwad172/coding/stambat-backend  (2026-04-21)

## Corpus Check
- 280 files · ~83,883 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 644 nodes · 705 edges · 101 communities detected
- Extraction: 83% EXTRACTED · 16% INFERRED · 0% AMBIGUOUS · INFERRED: 115 edges (avg confidence: 0.81)
- Token cost: 0 input · 0 output

## Community Hubs (Navigation)
- [[_COMMUNITY_Community 0|Community 0]]
- [[_COMMUNITY_Community 1|Community 1]]
- [[_COMMUNITY_Community 2|Community 2]]
- [[_COMMUNITY_Community 3|Community 3]]
- [[_COMMUNITY_Community 4|Community 4]]
- [[_COMMUNITY_Community 5|Community 5]]
- [[_COMMUNITY_Community 6|Community 6]]
- [[_COMMUNITY_Community 7|Community 7]]
- [[_COMMUNITY_Community 8|Community 8]]
- [[_COMMUNITY_Community 9|Community 9]]
- [[_COMMUNITY_Community 10|Community 10]]
- [[_COMMUNITY_Community 11|Community 11]]
- [[_COMMUNITY_Community 12|Community 12]]
- [[_COMMUNITY_Community 13|Community 13]]
- [[_COMMUNITY_Community 14|Community 14]]
- [[_COMMUNITY_Community 15|Community 15]]
- [[_COMMUNITY_Community 16|Community 16]]
- [[_COMMUNITY_Community 17|Community 17]]
- [[_COMMUNITY_Community 18|Community 18]]
- [[_COMMUNITY_Community 19|Community 19]]
- [[_COMMUNITY_Community 20|Community 20]]
- [[_COMMUNITY_Community 21|Community 21]]
- [[_COMMUNITY_Community 22|Community 22]]
- [[_COMMUNITY_Community 23|Community 23]]
- [[_COMMUNITY_Community 24|Community 24]]
- [[_COMMUNITY_Community 25|Community 25]]
- [[_COMMUNITY_Community 26|Community 26]]
- [[_COMMUNITY_Community 27|Community 27]]
- [[_COMMUNITY_Community 28|Community 28]]
- [[_COMMUNITY_Community 29|Community 29]]
- [[_COMMUNITY_Community 30|Community 30]]
- [[_COMMUNITY_Community 31|Community 31]]
- [[_COMMUNITY_Community 32|Community 32]]
- [[_COMMUNITY_Community 33|Community 33]]
- [[_COMMUNITY_Community 34|Community 34]]
- [[_COMMUNITY_Community 35|Community 35]]
- [[_COMMUNITY_Community 36|Community 36]]
- [[_COMMUNITY_Community 37|Community 37]]
- [[_COMMUNITY_Community 38|Community 38]]
- [[_COMMUNITY_Community 39|Community 39]]
- [[_COMMUNITY_Community 40|Community 40]]
- [[_COMMUNITY_Community 41|Community 41]]
- [[_COMMUNITY_Community 42|Community 42]]
- [[_COMMUNITY_Community 43|Community 43]]
- [[_COMMUNITY_Community 44|Community 44]]
- [[_COMMUNITY_Community 45|Community 45]]
- [[_COMMUNITY_Community 46|Community 46]]
- [[_COMMUNITY_Community 47|Community 47]]
- [[_COMMUNITY_Community 48|Community 48]]
- [[_COMMUNITY_Community 49|Community 49]]
- [[_COMMUNITY_Community 50|Community 50]]
- [[_COMMUNITY_Community 51|Community 51]]
- [[_COMMUNITY_Community 52|Community 52]]
- [[_COMMUNITY_Community 53|Community 53]]
- [[_COMMUNITY_Community 54|Community 54]]
- [[_COMMUNITY_Community 55|Community 55]]
- [[_COMMUNITY_Community 56|Community 56]]
- [[_COMMUNITY_Community 57|Community 57]]
- [[_COMMUNITY_Community 58|Community 58]]
- [[_COMMUNITY_Community 59|Community 59]]
- [[_COMMUNITY_Community 60|Community 60]]
- [[_COMMUNITY_Community 61|Community 61]]
- [[_COMMUNITY_Community 62|Community 62]]
- [[_COMMUNITY_Community 63|Community 63]]
- [[_COMMUNITY_Community 64|Community 64]]
- [[_COMMUNITY_Community 65|Community 65]]
- [[_COMMUNITY_Community 66|Community 66]]
- [[_COMMUNITY_Community 67|Community 67]]
- [[_COMMUNITY_Community 68|Community 68]]
- [[_COMMUNITY_Community 69|Community 69]]
- [[_COMMUNITY_Community 70|Community 70]]
- [[_COMMUNITY_Community 71|Community 71]]
- [[_COMMUNITY_Community 72|Community 72]]
- [[_COMMUNITY_Community 73|Community 73]]
- [[_COMMUNITY_Community 74|Community 74]]
- [[_COMMUNITY_Community 75|Community 75]]
- [[_COMMUNITY_Community 76|Community 76]]
- [[_COMMUNITY_Community 77|Community 77]]
- [[_COMMUNITY_Community 78|Community 78]]
- [[_COMMUNITY_Community 79|Community 79]]
- [[_COMMUNITY_Community 80|Community 80]]
- [[_COMMUNITY_Community 81|Community 81]]
- [[_COMMUNITY_Community 82|Community 82]]
- [[_COMMUNITY_Community 83|Community 83]]
- [[_COMMUNITY_Community 84|Community 84]]
- [[_COMMUNITY_Community 85|Community 85]]
- [[_COMMUNITY_Community 86|Community 86]]
- [[_COMMUNITY_Community 87|Community 87]]
- [[_COMMUNITY_Community 88|Community 88]]
- [[_COMMUNITY_Community 89|Community 89]]
- [[_COMMUNITY_Community 90|Community 90]]
- [[_COMMUNITY_Community 91|Community 91]]
- [[_COMMUNITY_Community 92|Community 92]]
- [[_COMMUNITY_Community 93|Community 93]]
- [[_COMMUNITY_Community 94|Community 94]]
- [[_COMMUNITY_Community 95|Community 95]]
- [[_COMMUNITY_Community 96|Community 96]]
- [[_COMMUNITY_Community 97|Community 97]]
- [[_COMMUNITY_Community 98|Community 98]]
- [[_COMMUNITY_Community 99|Community 99]]
- [[_COMMUNITY_Community 100|Community 100]]

## God Nodes (most connected - your core abstractions)
1. `ApiResponse<T>` - 20 edges
2. `AbstractValidator<T>` - 15 edges
3. `ICommandRoute<T>` - 14 edges
4. `ApplicationDbContext` - 11 edges
5. `CardTemplate` - 10 edges
6. `User` - 10 edges
7. `JwtService` - 10 edges
8. `IEndpointModule` - 9 edges
9. `BaseHandler<TRequest,TResponse>` - 9 edges
10. `GetAllCardsQueryHandler` - 9 edges

## Surprising Connections (you probably didn't know these)
- `_camelCase for private/internal fields` --conceptually_related_to--> `BaseHandler<TRequest,TResponse>`  [INFERRED]
  conductor/code_styleguides/csharp.md → src/Stambat.Application/CQRS/BaseHandler.cs
- `Loyalty Card Lifecycle` --conceptually_related_to--> `CreateCardCommandHandler`  [INFERRED]
  docs/business-rules.md → src/Stambat.Application/CQRS/CommandHandlers/Cards/CreateCardCommandHandler.cs
- `Loyalty Card Lifecycle` --conceptually_related_to--> `UpdateCardCommandHandler`  [INFERRED]
  docs/business-rules.md → src/Stambat.Application/CQRS/CommandHandlers/Cards/UpdateCardCommandHandler.cs
- `User Aggregate Root (rich domain model)` --conceptually_related_to--> `LoginCommandHandler`  [INFERRED]
  conductor/archive/refactor_identity_aggregate_20260321/spec.md → src/Stambat.Application/CQRS/CommandHandlers/Authentication/LoginCommandHandler.cs
- `User Aggregate Root (rich domain model)` --conceptually_related_to--> `LogoutCommandHandler`  [INFERRED]
  conductor/archive/refactor_identity_aggregate_20260321/spec.md → src/Stambat.Application/CQRS/CommandHandlers/Authentication/LogoutCommandHandler.cs

## Hyperedges (group relationships)
- **Identity Aggregate Cluster** — dbdiagram_user, dbdiagram_usercredentials, dbdiagram_userroletenant, dbdiagram_role, dbdiagram_permission, dbdiagram_refreshtoken, dbdiagram_usertoken [EXTRACTED 0.95]
- **Loyalty Stamping Flow** — dbdiagram_cardtemplate, dbdiagram_walletpass, dbdiagram_stamptransaction, product_qr_stamping, product_wallet_integration [EXTRACTED 0.90]
- **Clean Architecture Layered Dependency** — architecture_domain_layer, architecture_application_layer, architecture_infrastructure_layer, architecture_webapi_layer, architecture_dependency_flow [EXTRACTED 1.00]
- **Multi-tenant authentication flow (login, select tenant, switch tenant)** — logincommandhandler_authentication, selecttenantcommandhandler_authentication, switchtenantcommandhandler_authentication [INFERRED 0.90]
- **Card Template CRUD handlers operating on CardTemplate aggregate** — createcardcommandhandler_cards, updatecardcommandhandler_cards, deletecardcommandhandler_cards [INFERRED 0.92]
- **Conductor archived tracks (spec + plan + index) - governance pattern** — enforce_guid_v7_20260221_spec, refactor_identity_aggregate_20260321_spec, update_permissions_20260309_spec [INFERRED 0.80]
- **Invitation lifecycle (invite -> accept/join -> cancel)** — invitestaffcommandhandler_class, acceptinvitationcommandhandler_class, jointenantcommandhandler_class, cancelinvitationcommandhandler_class [INFERRED 0.90]
- **Staff role management (update/remove/deactivate)** — updatestaffrolescommandhandler_class, removestaffcommandhandler_class, deactivatestaffcommandhandler_class [INFERRED 0.90]
- **Wallet pass scan lifecycle (onboard -> stamp -> redeem)** — customeronboardcommandhandler_class, scanstampcommandhandler_class, scanredeemcommandhandler_class [INFERRED 0.90]
- **Tenant staff lifecycle (invite/accept/update/deactivate/remove)** — invitestaffcommand_command, acceptinvitationcommand_command, updatestaffrolescommand_command, deactivatestaffcommand_command, removestaffcommand_command, cancelinvitationcommand_command [INFERRED 0.85]
- **QR-based scanning flow (stamp and redeem)** — scanstampcommand_command, scanredeemcommand_command, customeronboardcommand_command [INFERRED 0.80]
- **Card template query cluster (handlers, queries, records)** — getcardbyidqueryhandler_handler, getallcardsqueryhandler_handler, getcardbyidquery_query, getallcardsquery_query, cardrecord_record, cardtemplate_entity [EXTRACTED 0.90]
- **Tenant staff query handlers cluster** — getalltenantstaffqueryhandler_class, getstaffmemberqueryhandler_class, getstaffrolesqueryhandler_class, gettenantinvitationsqueryhandler_class [INFERRED 0.85]
- **Application services layer** — currentuserservice_class, securityservice_class, jwtservice_class, tenantproviderservice_class, permissionservice_class [INFERRED 0.90]
- **JWT authentication and claims stack** — jwtservice_class, customclaims_class, securityservice_class, permissionservice_class, refreshtoken_entity [INFERRED 0.90]
- **Role-Based Access Control triad** — role_entity, permission_entity, rolepermission_entity, userroletenant_entity [EXTRACTED 0.95]
- **Loyalty Pass Core Aggregate** — walletpass_entity, cardtemplate_entity, stamptransaction_entity [EXTRACTED 0.95]
- **Tenant Context Cluster** — tenant_entity, tenantprofile_entity, invitation_entity [INFERRED 0.85]
- **Domain Exceptions** — invitationexpiredexception_class, invitationstillactiveexception_class, walletproviderexception_class [INFERRED 0.80]
- **Auditing Interfaces** — icreationaudit_interface, imodificationaudit_interface, isoftdelete_interface [INFERRED 0.90]
- **Application Service Interfaces** — ijwtservice_interface, ipermissionservice_interface, isecurityservice_interface [INFERRED 0.80]
- **Wallet Pass Provider Contract** — iwalletpassprovider_interface, iwalletpassproviderfactory_interface, walletclassrequest_record, walletpassrequest_record, walletpassupdaterequest_record [INFERRED 0.90]
- **Repository Pattern** — irepository_interface, iuserrepository_interface, itenantrepository_interface [INFERRED 0.90]
- **Domain Value Objects** — fullname_record, email_record, emailmessage_class [INFERRED 0.75]
- **EF Core Entity Configurations** — tenantconfiguration_cs_tenantconfiguration, userconfiguration_cs_userconfiguration, walletpassconfiguration_cs_walletpassconfiguration [INFERRED 0.85]
- **Authorization Seed Data** — permissionsseed_cs_permissionsseed, rolesseed_cs_rolesseed, authseedconstants_cs_authseedconstants [EXTRACTED 1.00]
- **Wallet Pass Integration Components** — googlewalletpassprovider_cs_googlewalletpassprovider, walletpassproviderfactory_cs_walletpassproviderfactory, googlewalletoptions_cs_googlewalletoptions [EXTRACTED 1.00]
- **Invitation/tenant email models** — merchantinvitationemailmodel_merchantinvitationemailmodel, teaminvitationemailmodel_teaminvitationemailmodel, tenantwelcomeemailmodel_tenantwelcomeemailmodel, verificationemailmodel_verificationemailmodel [INFERRED 0.85]
- **Migrations modifying WalletPasses table** — addwalletqrintegration_addwalletqrintegration, addexpirationperiodtocardtemplatesmigration_addexpirationperiodtocardtemplatesmigration, addredemptiontypeandrenamecolumns_addredemptiontypeandrenamecolumns, renameLaststampedatandremoveredemptioncount_renameLaststampedatandremoveredemptioncount [EXTRACTED 0.95]
- **Migrations modifying CardTemplates table** — addwalletqrintegration_addwalletqrintegration, addexpirationperiodtocardtemplatesmigration_addexpirationperiodtocardtemplatesmigration, addredemptiontypeandrenamecolumns_addredemptiontypeandrenamecolumns [EXTRACTED 0.95]
- **Concrete repositories implement Repository<T>** — tenant_repository_tenantrepository, invitation_repository_invitationrepository, role_repository_rolerepository, user_repository_userrepository [EXTRACTED 1.00]
- **Wallet pass schema migrations (Google Wallet + Stamps)** — migration_google_wallet_integration, migration_google_wallet_integration_v2, migration_google_wallet_integration_v3, migration_add_stamps_required_to_wallet_pass [EXTRACTED 1.00]
- **Auditing infrastructure participants** — auditing_interceptor_auditinginterceptor, application_db_context_applicationdbcontext, icreation_audit_icreationaudit, imodification_audit_imodificationaudit [INFERRED 0.85]
- **Invitation schema evolution migrations** — migration_add_invitation_is_cancelled, migration_card_management, migration_card_management_fix [INFERRED 0.80]
- **All IEndpointModule implementers** — invitationmodule_cs_class, authenticationmodule_cs_class, customeronboardingmodule_cs_class, platformadminmodule_cs_class, usermodule_cs_class, tenantmodule_cs_class, scanmodule_cs_class, cardmodule_cs_class [EXTRACTED 1.00]
- **Route contract interfaces (static abstract RegisterRoute)** — icommandroute_cs_interface, iqueryroute_cs_interface, iparameterizedqueryroute_cs_interface [INFERRED 0.90]
- **Repository<T> read-side query methods using ApplyOptions** — repository_cs_getallasync, repository_cs_getbyidasync, repository_cs_firstordefault [EXTRACTED 1.00]
- **Authentication route group** — login_route, logout_route, switchtenant_route, selecttenant_route [EXTRACTED 0.95]
- **Card CRUD route group** — getcardbyid_route, getallcards_route, createcard_route, updatecard_route, deletecard_route [EXTRACTED 0.95]
- **Invitation route group** — jointenant_route, acceptinvitation_route, validateinvitation_route [EXTRACTED 0.95]
- **Tenant management route group** — deactivatestaff_route, updatestaffroles_route, gettenantinvitations_route, setuptenant_route, getalltenantstaff_route [EXTRACTED 0.95]
- **Scanning route group** — scanredeem_route, scanstamp_route [EXTRACTED 0.95]
- **ICommandRoute implementers** — invitetenant_route, logout_route, switchtenant_route [EXTRACTED 1.00]
- **IParameterizedQueryRoute implementers** — getcardbyid_route, getallcards_route, validateinvitation_route [EXTRACTED 1.00]
- **Tenant staff management routes** — invitestaff_route, getstaffmember_route, removestaff_route, getstaffroles_route, cancelinvitation_route [EXTRACTED 0.90]
- **User registration and verification routes** — registeruser_route, isuserverified_route, verifyemail_route [EXTRACTED 0.90]
- **Authentication command validators** — logincommandvalidator_validator, logoutcommandvalidator_validator, switchtenantcommandvalidator_validator [EXTRACTED 0.90]
- **Tenant Staff Command/Query Validators** — removestaffcommandvalidator_class, deactivatestaffcommandvalidator_class, updatestaffrolescommandvalidator_class, invitestaffcommandvalidator_class, getstaffmemberqueryvalidator_class, getalltenantstaffqueryvalidator_class [EXTRACTED 0.95]
- **FluentValidation AbstractValidator Implementers** — scanredeemcommandvalidator_class, registerusercommandvalidator_class, customeronboardcommandvalidator_class [EXTRACTED 1.00]
- **User Identity & Verification Validators** — registerusercommandvalidator_class, verifyemailcommandvalidator_class, isuserverifiedqueryvalidator_class [INFERRED 0.85]

## Communities

### Community 0 - "Community 0"
Cohesion: 0.06
Nodes (53): AcceptInvitation, AcceptInvitationCommand, AcceptInvitationCommandResult, AcceptInvitationCommandValidator, ApiResponse<T>, CreateCard, CreateCardCommand, CreateCardCommandValidator (+45 more)

### Community 1 - "Community 1"
Cohesion: 0.07
Nodes (50): BaseHandler, ConfigurationExtensions, CustomClaims, CustomClaims, Email, EndpointRoutes, EndpointTags, EnvironmentVariableNotSetException (+42 more)

### Community 2 - "Community 2"
Cohesion: 0.07
Nodes (41): BaseHandler, CardRecord, CardTemplate, CardType, CurrentUserService, CustomerOnboard, CustomerOnboardCommand, CustomerOnboardCommandResult (+33 more)

### Community 3 - "Community 3"
Cohesion: 0.07
Nodes (33): AbstractValidator<T>, CustomerOnboardCommand, CustomerOnboardCommandValidator, DeactivateStaffCommand, DeactivateStaffCommandValidator, FluentValidation, GetAllCardsQuery, GetAllCardsQueryValidator (+25 more)

### Community 4 - "Community 4"
Cohesion: 0.1
Nodes (24): EF Core Fluent API Configurations, Per-Entity Configuration Rule, CardTemplate Entity, ER Diagram, Invitation Entity, Permission Entity, RefreshToken Entity, Role Entity (+16 more)

### Community 5 - "Community 5"
Cohesion: 0.09
Nodes (24): Stambat Loyalty Business Rules, Merchant Authorization and Audit, Migration: multiple-stamp support, Rewards and Redemption, Stamping Mechanics (QR zero-friction), Anti-fraud requires QR integrity and audit trails, Microsoft.CodeAnalysis.BannedApiAnalyzers, BannedSymbols.txt (+16 more)

### Community 6 - "Community 6"
Cohesion: 0.1
Nodes (23): AuthSeedConstants, CardTemplate, CardTemplateConfiguration, Invitation, InvitationConfiguration, Permission, PermissionConfiguration, PermissionsSeed (+15 more)

### Community 7 - "Community 7"
Cohesion: 0.12
Nodes (23): BaseHandler<TRequest,TResponse>, Loyalty Card Lifecycle, CreateCardCommandHandler, PascalCase naming convention, _camelCase for private/internal fields, Google C# Style Guide Summary, DeleteCardCommandHandler, Stambat.Application.DependencyInjection (+15 more)

### Community 8 - "Community 8"
Cohesion: 0.14
Nodes (19): ApplicationDbContext, AuditingInterceptor, AuthSeedConstants, DbInitializer.ApplyMigrationsAndSeedAsync, CardTemplate entity, UserRoleTenant entity, WalletPass entity, ICreationAudit (+11 more)

### Community 9 - "Community 9"
Cohesion: 0.12
Nodes (19): AuditingInterceptor, ICreationAudit Interface, ICurrentUserService, IModificationAudit Interface, Automated Auditing Track Index, Automated Auditing Plan, Rationale: centralize auditing, ensure consistency, Automated Auditing Spec (+11 more)

### Community 10 - "Community 10"
Cohesion: 0.12
Nodes (18): Invitation entity, Role entity, Tenant entity, User entity, IInvitationRepository, InvitationRepository, IRoleRepository, ITenantRepository (+10 more)

### Community 11 - "Community 11"
Cohesion: 0.14
Nodes (16): AuthenticationModule, CardModule, CustomerOnboardingModule, AddPresentation, WebAPI.DependencyInjection, EndpointExtensions.MapEndpointModules, ExceptionHandlerMiddleware, ExceptionHandlerMiddleware.HandleExceptionAsync (+8 more)

### Community 12 - "Community 12"
Cohesion: 0.17
Nodes (16): AcceptInvitationCommandHandler, CancelInvitationCommandHandler, CustomerOnboardCommandHandler, InviteStaffCommandHandler, InviteTenantCommand, JoinTenantCommand, JoinTenantCommandHandler, LoginCommand (+8 more)

### Community 13 - "Community 13"
Cohesion: 0.14
Nodes (14): docs/business-rules.md, Documentation Setup Track Index, Documentation Setup Plan, Documentation Setup Spec, macOS + .NET Aspire 502/500 Issue, appsettings.Development.json, Clean Architecture Principles, Domain-Driven Design (+6 more)

### Community 14 - "Community 14"
Cohesion: 0.22
Nodes (13): AppleWalletOptions, AddInfrastructure, GoogleWalletOptions, CreateClassAsync, CreatePassAsync, GoogleWalletPassProvider, UpdateClassAsync, UpdatePassAsync (+5 more)

### Community 15 - "Community 15"
Cohesion: 0.19
Nodes (13): AddExpirationPeriodToCardTemplatesMigration, AddRedemptionTypeAndRenameColumns, AddWalletQrIntegration, CardTemplates table, Permissions table, RolePermissions table, StampTransactions table, WalletPasses table (+5 more)

### Community 16 - "Community 16"
Cohesion: 0.23
Nodes (13): EmailService, EmailService.GetTemplatePath, IEmailService, IFluentEmail, EmailService.SendEmailAsync, EmailService.SendExistingUserAccessGrantAsync, EmailService.SendMerchantOnboardingInviteAsync, EmailService.SendTenantWelcomeEmailAsync (+5 more)

### Community 17 - "Community 17"
Cohesion: 0.25
Nodes (3): CardTemplate, IAggregateRoot, IBaseEntity

### Community 18 - "Community 18"
Cohesion: 0.2
Nodes (10): Graphify Project Rules, Project Context Index, Stambat Product Vision, Welcoming & Trustworthy Voice, .NET 10.0 / C#, Project Tracks Registry, Git Notes Task Summary, Phase Verification & Checkpointing Protocol (+2 more)

### Community 19 - "Community 19"
Cohesion: 0.22
Nodes (9): AuthenticationRepository, AuthRepo.GetAllPermissionNamesAsync, AuthRepo.GetBaseGrantedPermissionsAsync, AuthRepo.GetUserRoleIdsForTenantAsync, AuthRepo.GetUserRolesForTenantAsync, AuthRepo.GetUserRoleIdsAsync, AuthRepo.GetUserRolesAsync, AuthRepo.GetUserTenantsAsync (+1 more)

### Community 20 - "Community 20"
Cohesion: 0.25
Nodes (2): IDisposable, IUnitOfWork

### Community 21 - "Community 21"
Cohesion: 0.25
Nodes (1): IRepository

### Community 22 - "Community 22"
Cohesion: 0.32
Nodes (8): CancelInvitation, CancelInvitationCommand, CancelInvitationCommandResult, CancelInvitationCommandValidator, InviteStaff, InviteStaffCommand, InviteStaffCommandResult, RolesEnum

### Community 23 - "Community 23"
Cohesion: 0.32
Nodes (8): IsUserVerified, IsUserVerifiedQuery, IsUserVerifiedQueryResult, RegisterUser, RegisterUserCommand, VerifyEmail, VerifyEmailCommand, VerifyEmailCommandResult

### Community 24 - "Community 24"
Cohesion: 0.29
Nodes (7): GetAllTenantStaff, GetAllTenantStaffQuery, GetAllTenantStaffQueryResult, GetStaffMember, GetStaffMemberQuery, GetStaffMemberQueryResult, StaffRecord

### Community 25 - "Community 25"
Cohesion: 0.43
Nodes (7): Stambat.Application Layer, Strict Dependency Flow, Stambat.Domain Layer, Stambat.Infrastructure Layer, Project Structure, Repository + Unit of Work Pattern, Stambat.WebAPI Layer

### Community 26 - "Community 26"
Cohesion: 0.33
Nodes (6): IAggregateRoot, IBaseEntity, ICreationAudit, IEntity, IModificationAudit, ISoftDelete

### Community 27 - "Community 27"
Cohesion: 0.4
Nodes (5): Repository.ApplyOptions, Repository<T>, Repository.FirstOrDefaultAsync, Repository.GetAllAsync, Repository.GetByIdAsync

### Community 28 - "Community 28"
Cohesion: 0.4
Nodes (5): BaseHandler.cs, CQRS with MediatR, Rich Domain Model Pattern, Coding Do Rules, MediatR

### Community 29 - "Community 29"
Cohesion: 0.5
Nodes (4): AvailableRoleRecord, GetStaffRoles, GetStaffRolesQuery, GetStaffRolesQueryResult

### Community 30 - "Community 30"
Cohesion: 0.5
Nodes (2): AddIsActiveToUserRoleTenant, Stambat.Infrastructure.Migrations

### Community 31 - "Community 31"
Cohesion: 0.5
Nodes (2): GoogleWalletIntegrationMigration, Stambat.Infrastructure.Migrations

### Community 32 - "Community 32"
Cohesion: 0.5
Nodes (2): InitialMigration, Stambat.Infrastructure.Migrations

### Community 33 - "Community 33"
Cohesion: 0.5
Nodes (2): RenameLastStampedAtAndRemoveRedemptionCount, Stambat.Infrastructure.Migrations

### Community 34 - "Community 34"
Cohesion: 0.5
Nodes (2): FixingLoginProcessMigration, Stambat.Infrastructure.Migrations

### Community 35 - "Community 35"
Cohesion: 0.5
Nodes (2): AddWalletQrIntegration, Stambat.Infrastructure.Migrations

### Community 36 - "Community 36"
Cohesion: 0.5
Nodes (2): GoogleWalletIntegrationMigrationV3, Stambat.Infrastructure.Migrations

### Community 37 - "Community 37"
Cohesion: 0.5
Nodes (2): AddInvitationIsCancelled, Stambat.Infrastructure.Migrations

### Community 38 - "Community 38"
Cohesion: 0.5
Nodes (2): CardManagementMigration, Stambat.Infrastructure.Migrations

### Community 39 - "Community 39"
Cohesion: 0.5
Nodes (2): GoogleWalletIntegrationMigrationV2, Stambat.Infrastructure.Migrations

### Community 40 - "Community 40"
Cohesion: 0.5
Nodes (2): AddStampsRequiredToWalletPass, Stambat.Infrastructure.Migrations

### Community 41 - "Community 41"
Cohesion: 0.5
Nodes (2): AddExpirationPeriodToCardTemplatesMigration, Stambat.Infrastructure.Migrations

### Community 42 - "Community 42"
Cohesion: 0.5
Nodes (2): AddRedemptionTypeAndRenameColumns, Stambat.Infrastructure.Migrations

### Community 43 - "Community 43"
Cohesion: 0.5
Nodes (2): CardManagementFixMigration, Stambat.Infrastructure.Migrations

### Community 44 - "Community 44"
Cohesion: 0.67
Nodes (3): ICommandRoute<TRequest>, IParameterizedQueryRoute<TQuery>, IQueryRoute<TQuery>

### Community 45 - "Community 45"
Cohesion: 1.0
Nodes (3): EmailMessage, EmailSettings, IEmailService

### Community 46 - "Community 46"
Cohesion: 0.67
Nodes (3): DeactivateStaffCommandHandler, RemoveStaffCommandHandler, UpdateStaffRolesCommandHandler

### Community 47 - "Community 47"
Cohesion: 0.67
Nodes (3): CreateCardCommand, DeleteCardCommand, UpdateCardCommand

### Community 48 - "Community 48"
Cohesion: 0.67
Nodes (3): FixingLoginProcessMigration, RefreshTokens table, Tenants table

### Community 49 - "Community 49"
Cohesion: 0.67
Nodes (3): Standardized ApiResponse Rule, ApiResponse Class, Exception Handling Middleware

### Community 50 - "Community 50"
Cohesion: 0.67
Nodes (3): EF Core Change Tracking Gotcha, EF Core Change Tracking Issue, Entity Framework Core

### Community 51 - "Community 51"
Cohesion: 1.0
Nodes (2): InvitationExpiredException, InvitationStillActiveException

### Community 52 - "Community 52"
Cohesion: 1.0
Nodes (1): ITenantProviderService

### Community 53 - "Community 53"
Cohesion: 1.0
Nodes (1): ICurrentUserService

### Community 54 - "Community 54"
Cohesion: 1.0
Nodes (1): IModificationAudit

### Community 55 - "Community 55"
Cohesion: 1.0
Nodes (2): ApplicationDbContextModelSnapshot, InitialMigration

### Community 56 - "Community 56"
Cohesion: 1.0
Nodes (2): IdGenerator.New() Mandate, Rationale: UUID v7 time-ordered IDs

### Community 57 - "Community 57"
Cohesion: 1.0
Nodes (2): Husky.Net, Husky.Net Pre-commit/Pre-push Hooks

### Community 58 - "Community 58"
Cohesion: 1.0
Nodes (2): Definition of Done, Quality Gates Checklist

### Community 59 - "Community 59"
Cohesion: 1.0
Nodes (2): Minimal API Endpoints, ASP.NET Core Minimal API

### Community 60 - "Community 60"
Cohesion: 1.0
Nodes (1): SwaggerAuthExtension (commented out)

### Community 61 - "Community 61"
Cohesion: 1.0
Nodes (1): DependencyInjection (Domain)

### Community 62 - "Community 62"
Cohesion: 1.0
Nodes (0): 

### Community 63 - "Community 63"
Cohesion: 1.0
Nodes (0): 

### Community 64 - "Community 64"
Cohesion: 1.0
Nodes (0): 

### Community 65 - "Community 65"
Cohesion: 1.0
Nodes (0): 

### Community 66 - "Community 66"
Cohesion: 1.0
Nodes (0): 

### Community 67 - "Community 67"
Cohesion: 1.0
Nodes (1): NotActiveUserException

### Community 68 - "Community 68"
Cohesion: 1.0
Nodes (1): WalletProviderException

### Community 69 - "Community 69"
Cohesion: 1.0
Nodes (1): UserNotVerifiedException

### Community 70 - "Community 70"
Cohesion: 1.0
Nodes (1): BusinessRuleException

### Community 71 - "Community 71"
Cohesion: 1.0
Nodes (1): DeletedUserException

### Community 72 - "Community 72"
Cohesion: 1.0
Nodes (1): CustomValidationException

### Community 73 - "Community 73"
Cohesion: 1.0
Nodes (1): ConflictException

### Community 74 - "Community 74"
Cohesion: 1.0
Nodes (1): PaginationResult

### Community 75 - "Community 75"
Cohesion: 1.0
Nodes (1): QueryOptions<T>

### Community 76 - "Community 76"
Cohesion: 1.0
Nodes (1): IQrCodeService

### Community 77 - "Community 77"
Cohesion: 1.0
Nodes (1): MapsterConfiguration

### Community 78 - "Community 78"
Cohesion: 1.0
Nodes (1): BaseService

### Community 79 - "Community 79"
Cohesion: 1.0
Nodes (1): DependencyInjection

### Community 80 - "Community 80"
Cohesion: 1.0
Nodes (1): Users table

### Community 81 - "Community 81"
Cohesion: 1.0
Nodes (1): Repository.AddAsync

### Community 82 - "Community 82"
Cohesion: 1.0
Nodes (1): Repository.DeleteAsync

### Community 83 - "Community 83"
Cohesion: 1.0
Nodes (1): Repository.Update

### Community 84 - "Community 84"
Cohesion: 1.0
Nodes (1): Tenant Admin Actor

### Community 85 - "Community 85"
Cohesion: 1.0
Nodes (1): Employee Actor

### Community 86 - "Community 86"
Cohesion: 1.0
Nodes (1): Customer Actor

### Community 87 - "Community 87"
Cohesion: 1.0
Nodes (1): MVP Goal & Constraint

### Community 88 - "Community 88"
Cohesion: 1.0
Nodes (1): Squash and Rebase Merge Strategy

### Community 89 - "Community 89"
Cohesion: 1.0
Nodes (1): Conventional Commit Format

### Community 90 - "Community 90"
Cohesion: 1.0
Nodes (1): PostgreSQL

### Community 91 - "Community 91"
Cohesion: 1.0
Nodes (1): Mapster

### Community 92 - "Community 92"
Cohesion: 1.0
Nodes (1): FluentValidation

### Community 93 - "Community 93"
Cohesion: 1.0
Nodes (1): PermissionService

### Community 94 - "Community 94"
Cohesion: 1.0
Nodes (1): Swagger/OpenAPI

### Community 95 - "Community 95"
Cohesion: 1.0
Nodes (1): Microsoft.CodeAnalysis.BannedApiAnalyzers

### Community 96 - "Community 96"
Cohesion: 1.0
Nodes (1): BannedSymbols.txt Enforcement

### Community 97 - "Community 97"
Cohesion: 1.0
Nodes (1): Stambat Branding First

### Community 98 - "Community 98"
Cohesion: 1.0
Nodes (1): Coding Don't Rules

### Community 99 - "Community 99"
Cohesion: 1.0
Nodes (1): Stambat.Domain.csproj.FileListAbsolute

### Community 100 - "Community 100"
Cohesion: 1.0
Nodes (1): Infrastructure.csproj.FileListAbsolute

## Ambiguous Edges - Review These
- `EndpointTags` → `BaseHandler`  [AMBIGUOUS]
  src/Stambat.Domain/Constants/EndpointTags.cs · relation: conceptually_related_to
- `ScanStampCommandHandler` → `InviteStaffCommandHandler`  [AMBIGUOUS]
  src/Stambat.Application/CQRS/CommandHandlers/Scanning/ScanStampCommandHandler.cs · relation: conceptually_related_to

## Knowledge Gaps
- **250 isolated node(s):** `WebAPI.DependencyInjection`, `SwaggerAuthExtension (commented out)`, `InvitationModule`, `CustomerOnboardingModule`, `PlatformAdminModule` (+245 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **Thin community `Community 51`** (2 nodes): `InvitationExpiredException`, `InvitationStillActiveException`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 52`** (2 nodes): `ITenantProviderService`, `ITenantProviderService.cs`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 53`** (2 nodes): `ICurrentUserService`, `ICurrentUserService.cs`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 54`** (2 nodes): `IModificationAudit`, `IModificationAudit.cs`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 55`** (2 nodes): `ApplicationDbContextModelSnapshot`, `InitialMigration`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 56`** (2 nodes): `IdGenerator.New() Mandate`, `Rationale: UUID v7 time-ordered IDs`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 57`** (2 nodes): `Husky.Net`, `Husky.Net Pre-commit/Pre-push Hooks`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 58`** (2 nodes): `Definition of Done`, `Quality Gates Checklist`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 59`** (2 nodes): `Minimal API Endpoints`, `ASP.NET Core Minimal API`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 60`** (1 nodes): `SwaggerAuthExtension (commented out)`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 61`** (1 nodes): `DependencyInjection (Domain)`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 62`** (1 nodes): `InvitationStatus.cs`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 63`** (1 nodes): `RedemptionType.cs`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 64`** (1 nodes): `WalletProviderType.cs`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 65`** (1 nodes): `RolesEnum.cs`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 66`** (1 nodes): `CardType.cs`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 67`** (1 nodes): `NotActiveUserException`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 68`** (1 nodes): `WalletProviderException`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 69`** (1 nodes): `UserNotVerifiedException`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 70`** (1 nodes): `BusinessRuleException`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 71`** (1 nodes): `DeletedUserException`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 72`** (1 nodes): `CustomValidationException`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 73`** (1 nodes): `ConflictException`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 74`** (1 nodes): `PaginationResult`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 75`** (1 nodes): `QueryOptions<T>`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 76`** (1 nodes): `IQrCodeService`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 77`** (1 nodes): `MapsterConfiguration`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 78`** (1 nodes): `BaseService`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 79`** (1 nodes): `DependencyInjection`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 80`** (1 nodes): `Users table`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 81`** (1 nodes): `Repository.AddAsync`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 82`** (1 nodes): `Repository.DeleteAsync`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 83`** (1 nodes): `Repository.Update`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 84`** (1 nodes): `Tenant Admin Actor`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 85`** (1 nodes): `Employee Actor`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 86`** (1 nodes): `Customer Actor`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 87`** (1 nodes): `MVP Goal & Constraint`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 88`** (1 nodes): `Squash and Rebase Merge Strategy`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 89`** (1 nodes): `Conventional Commit Format`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 90`** (1 nodes): `PostgreSQL`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 91`** (1 nodes): `Mapster`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 92`** (1 nodes): `FluentValidation`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 93`** (1 nodes): `PermissionService`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 94`** (1 nodes): `Swagger/OpenAPI`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 95`** (1 nodes): `Microsoft.CodeAnalysis.BannedApiAnalyzers`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 96`** (1 nodes): `BannedSymbols.txt Enforcement`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 97`** (1 nodes): `Stambat Branding First`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 98`** (1 nodes): `Coding Don't Rules`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 99`** (1 nodes): `Stambat.Domain.csproj.FileListAbsolute`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.
- **Thin community `Community 100`** (1 nodes): `Infrastructure.csproj.FileListAbsolute`
  Too small to be a meaningful cluster - may be noise or needs more connections extracted.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **What is the exact relationship between `EndpointTags` and `BaseHandler`?**
  _Edge tagged AMBIGUOUS (relation: conceptually_related_to) - confidence is low._
- **What is the exact relationship between `ScanStampCommandHandler` and `InviteStaffCommandHandler`?**
  _Edge tagged AMBIGUOUS (relation: conceptually_related_to) - confidence is low._
- **Why does `ApiResponse<T>` connect `Community 0` to `Community 24`, `Community 1`, `Community 2`?**
  _High betweenness centrality (0.034) - this node is a cross-community bridge._
- **Why does `GetAllCardsQueryHandler` connect `Community 2` to `Community 1`?**
  _High betweenness centrality (0.013) - this node is a cross-community bridge._
- **What connects `WebAPI.DependencyInjection`, `SwaggerAuthExtension (commented out)`, `InvitationModule` to the rest of the system?**
  _250 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Community 0` be split into smaller, more focused modules?**
  _Cohesion score 0.06 - nodes in this community are weakly interconnected._
- **Should `Community 1` be split into smaller, more focused modules?**
  _Cohesion score 0.07 - nodes in this community are weakly interconnected._