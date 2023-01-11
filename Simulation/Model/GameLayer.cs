using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Simulation.Model.Agent;
using Mars.Components.Environments.Cartesian;
using Mars.Components.Environments.Cartesian.Routing;
using Mars.Components.Layers;
using Mars.Core.Data;
using Mars.Interfaces.Data;
using Mars.Interfaces.Layers;
using NetTopologySuite.Geometries;
using Utils;

namespace Simulation.Model
{
    public class GameLayer : AbstractLayer
    {
        public Simulation GetSimuliation()
        {
            return _boundSimulation;
        }


        //[MethodImpl(MethodImplOptions.Synchronized)]
        public bool IsOOB(Vector2 vec)
        {
            var explore = Environment.ExploreObstacles(new Point(new Coordinate(vec.X, vec.Y))).ToList();
            //Console.WriteLine(vec.X + " && " + vec.Y+ " => " + explore.Count);
            return !(explore != null && explore.Count == 2);
        }

        public override bool InitLayer(LayerInitData layerInitData, RegisterAgent registerAgent = null, UnregisterAgent unregisterAgent = null)
        {
            base.InitLayer(layerInitData, registerAgent, unregisterAgent);
            _boundSimulation = Simulation.GetCurrentSimulation();

            GeoJson.GeoJsonObject trackGeoJson = _boundSimulation.GetTrack();

            var inputs = layerInitData.LayerInitConfig.Inputs;

            if (inputs != null && inputs.Any()) // trackGeoJson same info here
                // TODO: make editor load config JSON
            {
                // The config.json contains the information for the collision environment.
                Environment = new CollisionEnvironment<Player, Obstacle>();
            }

            //iterate 2 features
            foreach (var feature in trackGeoJson.Features)
            {
                //geometry coordinates, containing the first geoary being the Border/Background
                foreach (var geoary in feature.Geometry.Coordinates)
                {
                   
                    List<Coordinate> coords = new List<Coordinate>();
                    
                    foreach (var geo in geoary)
                    {
                        
                        coords.Add(new Coordinate(geo[0], geo[1]));

                    }
                    
                    Environment.Insert(new Obstacle(), new Polygon(new LinearRing(coords.ToArray())));
                }
                
            }

            Environment.RoutingPointsGenerator = new GeometryCoordinatesRoutingPointsGenerator(0.5);

            var agentManager = layerInitData.Container.Resolve<IAgentManager>();
            agentManager.Spawn<Player, GameLayer>().ToList();

            return true;
        }

        public CollisionEnvironment<Player, Obstacle> Environment { get; set; }
        private Simulation _boundSimulation;
    }
}