# Project Overview: Stambat (Stamply)

## What is Stambat?
Stambat is a modern digital loyalty card system that replaces physical stamp cards. It enables businesses to create and manage digital loyalty programs integrated with Apple Wallet and Google Wallet. Customers earn stamps and redeem rewards by scanning QR codes on their digital cards.

## Repository
- **Repo:** `git@github.com:aawwad172/stambat-backend.git`
- **Main branch:** `main`

## Business Domain
- Businesses define loyalty card rules (total stamps, rewards, branding)
- Users add loyalty cards to Apple/Google Wallet
- QR code scanning for zero-friction stamping
- Reward redemption via scan-to-validate flow
- Multi-stamp migration support for businesses transitioning from physical cards

## Actor Types
- **Super Admin (Platform Admin):** Manages the entire platform
- **Admin (Tenant Admin/Business Owner):** Manages their own business/tenant
- **Employee (Staff):** Operates at the store level (scanning, stamping)
- **Customer (End User):** Uses loyalty cards via wallet

## Multi-Tenancy & Authentication
- The system is multi-tenant — each business is a tenant
- Users can belong to multiple tenants with different roles
- **Authentication flow: 2-step login**
  1. User authenticates with credentials (email/password)
  2. After authentication, user selects which tenant to operate under
- This allows a single user to work across multiple businesses without separate accounts
