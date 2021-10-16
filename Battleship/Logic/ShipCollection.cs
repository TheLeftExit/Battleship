using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
    public class ShipCollection {
        public int[] Counts { get; } // Counts[x] is the amount of ships available with Size: x

        public ShipCollection(params int[] config) {
            Counts = config;
        }

        public IEnumerable<Size> ShipsAvailable() {
            if (Counts.Length > 0 && Counts[0] != 0)
                yield return new Size(0, 0);
            for (int i = 1; i < Counts.Length; i++)
                if (Counts[i] != 0) {
                    yield return new Size(0, i);
                    yield return new Size(i, 0);
                }
        }

        public void Take(Size ship) {
            int size = ship.Width | ship.Height;
            if (size > Counts.Length || Counts[size] <= 0)
                throw new ArgumentException();
            Counts[size]--;
        }

        public bool IsEmpty() => Counts.All(x => x <= 0);
    }
}
