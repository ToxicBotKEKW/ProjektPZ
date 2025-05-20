using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Projekt_Lab_11___12.Models.Entities;

public class User : IdentityUser
{
    public int? IronMineId { get; set; }
    public IronMine IronMine { get; set; } = new IronMine();

    public int? GoldMineId { get; set; }
    public GoldMine GoldMine { get; set; } = new GoldMine();

    public int? DiamondMineId { get; set; }
    public DiamondMine DiamondMine { get; set; } = new DiamondMine();

    public int? UsedPickaxeId { get; set; } = 1;
    public Pickaxe UsedPickaxe { get; set; }
    public List<Pickaxe> PickaxesEq { get; set; } = new();

    public double Iron { get; set; } = 0.0;
    public double Gold { get; set; } = 0.0;
	public double Diamond { get; set; } = 0.0;


}

