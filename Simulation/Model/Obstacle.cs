using System;
using Mars.Components.Environments.Cartesian;

namespace Simulation.Model
{
    public class Obstacle : IObstacle
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public bool IsRoutable(ICharacter character)
        {
            return false;
        }

        public CollisionKind? HandleCollision(ICharacter character)
        {
            return CollisionHandle?.Invoke(character);
        }
        public Func<ICharacter, CollisionKind?> CollisionHandle { get; set; }
        public VisibilityKind? HandleExploration(ICharacter explorer)
        {
            return ExplorationHandle?.Invoke(explorer);
        }
        public Func<ICharacter, VisibilityKind?> ExplorationHandle { get; set; }
    }
}