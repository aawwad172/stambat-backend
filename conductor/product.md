# Initial Concept
A digital loyalty card system for businesses that replaces physical cards. It allows businesses to define loyalty cards that users can add to their Apple and Google wallets. Stamps are added after each purchase by scanning a QR code on the user's card.

---

# Product Guide: Stambat - Digital Loyalty System

## Vision
Stambat aims to revolutionize the loyalty card experience by replacing physical cards with a seamless digital solution. We empower businesses to offer loyalty programs that are easily accessible to users through their Apple and Google wallets, enhancing customer engagement and simplifying the stamp process through QR code scanning.

## Target Audience & Actors
- **Super Admin (Platform Admin):** Manages the entire platform.
- **Admin (Tenant Admin/Business Owner):** Manages their own business/tenant.
- **Employee (Staff):** Operates at the store level (scanning, stamping).
- **Customer (End User):** Uses loyalty cards via wallet.

## Multi-Tenancy & Authentication
- The system is multi-tenant — each business is a separate tenant.
- Users can belong to multiple tenants with different roles.
- **Authentication flow: 2-step login**
  1. User authenticates with credentials (email/password).
  2. After authentication, user selects which tenant to operate under.
- This allows a single user to work across multiple businesses without separate accounts.

## Core Features
- **Digital Wallet Integration:** Support for Apple Wallet and Google Wallet.
- **Dynamic Card Definition:** Businesses define rules, branding, and rewards.
- **QR Code Stamping:** Zero-friction stamping via scanning unique QR codes.
- **Advanced Identity Management:** Granular role/permission system.
- **Database Excellence:** PostgreSQL with EF Core and UUID v7 (time-ordered IDs).

## Goals & Constraints
- **Goal:** Deliver a "good enough" MVP that balances high engineering standards with core functionality.
- **Constraint:** Maintain a strict separation of concerns following DDD and Clean Architecture.
- **ID Strategy:** Always use `IdGenerator.New()` (UUID v7) for new IDs, assigned in code.
