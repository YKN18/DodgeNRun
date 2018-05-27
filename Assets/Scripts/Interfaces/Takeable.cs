//Interface for GameObjects that can be collected (coins, powerups)
public interface Takeable<T> {
    T GetBonus();
}
