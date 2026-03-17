# Business Rules: Stambat Loyalty System

This document outlines the core business rules and mechanics of the Stambat digital loyalty card system.

## 1. Loyalty Card Lifecycle

### 1.1 Card Definition
- Businesses define the "rules" of their loyalty card, including:
    - **Total Stamps:** The number of stamps required to earn a reward (e.g., 10 stamps).
    - **Reward Description:** What the customer receives upon completing the card (e.g., "Free Coffee").
    - **Branding:** Merchant name, logo, and primary colors.

### 1.2 Card Issuance
- Users can add a merchant's loyalty card to their mobile wallet (Apple Wallet or Google Wallet).
- Each card is unique to the user and the merchant.

## 2. Stamping Mechanics

### 2.1 QR Code Identification
- Each digital loyalty card contains a unique QR code.
- This QR code serves as the primary identifier for the user's specific card instance.

### 2.2 The Stamping Process (Zero-Friction)
1. **Customer Presents Card:** The user opens their Apple or Google Wallet and presents the QR code.
2. **Merchant Scans QR:** The merchant (or staff member) scans the QR code using the Stambat merchant application or integrated scanner.
3. **Stamp Added:** Upon a successful scan and validation of the merchant's authority, a single stamp is added to the user's digital card backend record.
4. **Wallet Update:** The digital card in the user's wallet is automatically updated (via push notifications/wallet updates) to reflect the new stamp count.

## 3. Rewards and Redemption

### 3.1 Reward Trigger
- When the stamp count reaches the "Total Stamps" defined for the card, the card is marked as "Complete" or "Reward Ready".
- The user is notified via their digital wallet.

### 3.2 Redemption
- The reward is redeemed at the merchant's location.
- The redemption process follows a similar "scan-to-validate" flow to ensure the reward is used correctly and marked as redeemed in the system.

## 4. Security and Integrity

### 4.1 Merchant Authorization
- Only authorized staff members with the appropriate permissions can add stamps or redeem rewards.
- Every transaction (stamp/redemption) is logged for auditing purposes.

### 4.2 Anti-Fraud and Migration Support
- **QR Code Integrity:** QR codes are securely generated and validated to prevent unauthorized stamping.
- **Migration Flexibility:** To support businesses migrating from physical cards or other digital systems, the Stambat system allows for adding **multiple stamps at once** during the initial setup or migration phase. This ensures users do not lose their existing progress.
- **Audit Trails:** Every stamping action, whether single or multiple, is logged with the merchant ID and timestamp for fraud prevention and auditing.
