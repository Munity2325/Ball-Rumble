
namespace UnitAction {
    public class UnitAction {
        public enum Types {
            NONE,
            RUN,
            THROW,
            KICK,
            JUMP
        }

        public Types type;
        public uint force;
        public double direction;
        public double verticalAngle;

        public UnitAction(Types actionType, uint actionForce, double relativeDirection=0, double verticalAngle=0) {
            type = actionType;
            force = (actionForce <= 100) ? actionForce : 100;
            direction = relativeDirection;
            this.verticalAngle = verticalAngle;
        }
    }
}
