**Block Combat Mechanic**

### Overview
Block is a defensive mechanic in the game that absorbs incoming damage. Block is temporary by default and resets at the start of the respective unit’s turn unless specified otherwise. It applies to both the player and enemies.

---

### **Player Block Behavior**
1. **Gaining Block**:
   - The player can gain block during their turn by playing cards or triggering effects.

2. **Damage Absorption**:
   - Block absorbs incoming damage during the enemy’s turn until it is depleted or the enemy ends its attacks.

3. **Block Reset**:
   - Any remaining block resets to **0** at the start of the player’s next turn.
   - Exceptions: If the player has a trinket or effect that allows **block retention**, block may persist partially or fully into the next turn.

#### Example:
- **Turn 1 (Player Turn)**: Player gains 10 block.
- **Turn 1 (Enemy Turn)**: Enemy attacks for 6 damage. Block absorbs 6 damage, leaving 4 block.
- **Turn 2 (Start of Player Turn)**: Remaining 4 block resets to 0 (unless retained).

---

### **Enemy Block Behavior**
1. **Gaining Block**:
   - Enemies can gain block during their turn through abilities or effects.

2. **Damage Absorption**:
   - Block absorbs incoming damage during the player’s turn until it is depleted or the player ends their attacks.

3. **Block Reset**:
   - By default, any remaining block resets to **0** at the start of the enemy’s next turn.
   - Exceptions: Certain enemies may have abilities or mechanics allowing them to **retain block** between turns.

#### Example:
- **Turn 1 (Enemy Turn)**: Enemy gains 8 block.
- **Turn 1 (Player Turn)**: Player attacks for 10 damage. Block absorbs 8 damage, and 2 damage goes to the enemy’s health.
- **Turn 2 (Start of Enemy Turn)**: Remaining block resets to 0.

---

### **Block Retention Mechanics**
Block retention allows a unit to carry over block between turns. This is not the default behavior and must be explicitly specified by trinkets, abilities, or enemy mechanics.

1. **Player Block Retention**:
   - Certain trinkets or cards may allow the player to retain block partially or fully.
   - Example: "Retain up to 5 block between turns."

2. **Enemy Block Retention**:
   - Some enemies may have persistent block mechanics.
   - Example: An enemy with a "Fortified" trait retains all block until it is fully depleted.

---

### **Strategic Implications**
1. **Default Block Behavior**:
   - Forces players to plan their block usage turn-by-turn.
   - Emphasizes the importance of timing when using defensive or offensive cards.

2. **Block Retention**:
   - Adds depth by allowing more flexible or long-term defensive strategies.
   - Encourages players to prioritize enemies with persistent block abilities.

---

### **Edge Cases and Clarifications**
1. **Multiple Sources of Damage**:
   - Block is applied cumulatively against all incoming damage until depleted.

2. **Overkill Damage**:
   - Damage that exceeds the remaining block value reduces health by the excess amount.

3. **Conditional Block Effects**:
   - Block retention effects (e.g., "Retain X block") take priority over the default reset behavior.

4. **Status Effects**:
   - Block does not protect against non-damage effects such as debuffs or poison unless explicitly stated.


---

### **Related Mechanics**
- **Damage Calculation**: Block is applied before health is reduced.
- **Trinkets and Abilities**:
  - "Elastic Buffers": Retain up to 5 block between turns.
  - "Throttling Resolver": Reduces block loss when hit with high damage.

---