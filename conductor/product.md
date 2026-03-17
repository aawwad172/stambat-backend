# Initial Concept
A digital loyalty card system for businesses that replaces physical cards. It allows businesses to define loyalty cards that users can add to their Apple and Google wallets. Stamps are added after each purchase by scanning a QR code on the user's card.

---

# Product Guide: Stambat - Digital Loyalty System

## Vision
Stambat aims to revolutionize the loyalty card experience by replacing physical cards with a seamless digital solution. We empower businesses to offer loyalty programs that are easily accessible to users through their Apple and Google wallets, enhancing customer engagement and simplifying the stamp process through QR code scanning.

## Target Audience
- **Businesses (Retail/Service):** Looking for a modern, efficient, and cost-effective way to manage customer loyalty programs.
- **End Users:** Customers who want a convenient, digital-first way to track and redeem their loyalty rewards without carrying physical cards.

## Core Features
- **Digital Wallet Integration:** Support for Apple Wallet and Google Wallet, ensuring loyalty cards are always accessible.
- **Dynamic Card Definition:** Businesses can define their own loyalty card rules, branding, and reward structures.
- **QR Code Stamping:** A secure and quick method for businesses to add stamps to a customer's card by scanning a unique QR code.
- **Advanced Identity Management:** Robust authentication, refresh tokens, and a granular role/permission system to manage business owners, staff, and potentially end-users.
- **Database Excellence:** A reliable and scalable database schema managed with EF Core and PostgreSQL, including comprehensive seeding for initial setup.
- **Strict Architecture:** Adherence to Clean Architecture and DDD principles to ensure long-term maintainability and scalability, even for an MVP.

## Goals & Constraints
- **Goal:** Deliver a "good enough" MVP that balances high engineering standards with the core functionality needed to start selling to businesses.
- **Constraint:** Maintain a strict separation of concerns following DDD and Clean Architecture.
- **Testing:** While reliability is a priority, the initial MVP will focus on core functionality over comprehensive test case coverage.
