using Unity.Entities;
using Unity.Physics.Systems;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public partial class UserPhysicsGroup : CustomPhysicsSystemGroup
    {
        public UserPhysicsGroup() : base(1, true) {} // our physics group will simulate entities with world index 1, and share static entities with main world        
    }