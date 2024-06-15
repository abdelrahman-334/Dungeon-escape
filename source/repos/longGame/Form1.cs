using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace longGame
{
    public partial class Form1 : Form
    {
        //classes
        public class Adventurer
        {
            public int curr, side, cnd,idle,jump,attack,hp,attacked=0;
            public Rectangle dst, src;
            public List<Bitmap> FwdMove = new List<Bitmap>();
            public List<Bitmap> BwdMove = new List<Bitmap>();
            public List<Bitmap> FwdIdle = new List<Bitmap>();
            public List<Bitmap> BwdIdle = new List<Bitmap>();
            public List<Bitmap> FwdAttack = new List<Bitmap>();
            public List<Bitmap> BwdAttack = new List<Bitmap>();
            public List<Bitmap> FwdJump = new List<Bitmap>();
            public List<Bitmap> BwdJump = new List<Bitmap>();
            public List<Bitmap> Death = new List<Bitmap>();
            public List<Bitmap> BwdDeath = new List<Bitmap>();
            public List<Bitmap> climb = new List<Bitmap>();
        }
        public class Laser
        {
            public int curr, side;
            public Rectangle dst, src;
            public List<Bitmap> Fwdimg = new List<Bitmap>();
            public List<Bitmap> Bwdimg = new List<Bitmap>();
        }
        public class Bg 
        {
            public Rectangle dst, src;
            public Bitmap img;
        }
        public class tile
        {
            public Rectangle dst, src;
            public Bitmap img;
        }
        public class bullet
        {
            public Rectangle dst, src;
            public List<Bitmap> img= new List<Bitmap>();
            public List<Bitmap> revImg= new List<Bitmap>();
            public int lifetime,dir,curr,cnd;
        }
        public class Iz 
        {
            public Rectangle dst, src;
            public List<Bitmap> img=new List<Bitmap>();
            public List<Bitmap> fwdimg = new List<Bitmap>();

        }
        public class elevator 
        {
            public Rectangle dst, src;
            public Bitmap img;
            public int move,cnd,max,min;
        }
        public class ladder 
        {
            public Rectangle dst, src;
            public Bitmap img;
        }
        public class Wraith 
        {
            public Rectangle dst, src;
            public List<Bitmap> nonAgro=new List<Bitmap>();
            public List<Bitmap> Agro = new List<Bitmap>();
            public List<Bitmap> death = new List<Bitmap>();
            public List<Bitmap> Attack = new List<Bitmap>();
            public List<Bitmap> BwdnonAgro = new List<Bitmap>();
            public List<Bitmap> BwdAgro = new List<Bitmap>();
            public List<Bitmap> Bwddeath = new List<Bitmap>();
            public List<Bitmap> BwdAttack = new List<Bitmap>();
            public int range, aggro, side, move, idle,attack,agroAnim=0,max,cnd,rev,attacked,hp=5,hit=0;
        }
        public class skeleton
        {
            public Rectangle dst, src;
            public List<Bitmap> walk = new List<Bitmap>();
            public List<Bitmap> walkBwd = new List<Bitmap>();
            public List<Bitmap> Hit = new List<Bitmap>();
            public List<Bitmap> HitBwd = new List<Bitmap>();
            public List<Bitmap> death = new List<Bitmap>();
            public List<Bitmap> Attack = new List<Bitmap>();
            public List<Bitmap> Bwddeath = new List<Bitmap>();
            public List<Bitmap> BwdAttack = new List<Bitmap>();
            public int side, move,attack, agroAnim = 0, max, cnd, rev, attacked, hp = 2, hit = 0,idle=0,dead=0;
        }
        public class key 
        {
            public Rectangle dst, src;
            public List<Bitmap> img= new List<Bitmap>();
            public int cnd=0,curr=0;
        }
        public class door 
        {
            public Rectangle dst, src;
            public List<Bitmap> img = new List<Bitmap>();
            public int cnd = 0;
        }
        //buffer
        Bitmap buffer;
        //movement
        int up, down, left, right, speed = 3, jumpCt = 0;
        //Lists/character
        Adventurer adventurer;
        Laser laser;
        List<ladder> ladders = new List<ladder>();
        List<Bg> bg = new List<Bg>();
        List<tile> tiles = new List<tile>();
        Laser beam=new Laser();
        List<bullet> bullets = new List<bullet>();
        Iz iz=new Iz();
        List<Wraith> wraiths = new List<Wraith>();
        List<elevator> elevators = new List<elevator>();
        List<skeleton> skeletons = new List<skeleton>();
        List<key> keys = new List<key>();
        List<door> doors = new List<door>();
        //game-changing variables
        int upward =0;
        int laserCnd = 0;
        int Moveframes = 1,move=0,interval=0,gravity=0,airbourne=0;
        int tilenum = 600;
        int attackCd = 0;
        int onLadder = 0;
        int gameOver = 0;
        int climb = 0;
        int shooterCnd=-1;
        Graphics g2;

        public Form1()
        {
            //this.Size = new Size(1000, 700);
            //form editing
            WindowState = FormWindowState.Maximized;
            buffer = new Bitmap(Screen.GetWorkingArea(this).Width, Screen.GetWorkingArea(this).Height);
            BackColor = Color.Black;
            //object creation
            createAdventurer();
            //laser Ininitialize
            createLaserBwd(0, 0);
            createLaserFwd(0,0);
            //createLadder
            createLadder(300 , 600);
            createLadder(1600, 600);
            //createBg
            createBg();
            //createTiles
            createTile(0,700,50,50,600);
            createTile(100, 570, 30, 30,30);
            createTile(950,400,30,30,10);
            createTile(1200, 570, 30, 30, 30);
            createTile(1400, 400, 30, 30, 10);
            //createElevators
            createEle(870, 400, 130, 0);
            createEle(1200, 230, 120, 0);
            createEle(1470, 230, 120, 0);
            createEle(1770, 400, 120, 0);
            createEle(1770, 270, 130, 0);
            createEle(1770, 220, 50, 0);
            //createSkeletons
            createSkeleton(500, 500, 0, 100);
            createSkeleton(600, 500,0, 100);
            createSkeleton(700, 500, 0, 100);
            createSkeleton(800, 500, 0, 100);
            createSkeleton(1500, 500, 0, 100);
            createSkeleton(1550, 500, 0, 100);
            createSkeleton(1600, 500, 0, 100);
            createSkeleton(1650, 500, 0, 100);
            createSkeleton(1700, 500, 0, 100);
            createSkeleton(1750, 500, 0, 100);
            createSkeleton(1850, 500, 0, 100);
            createSkeleton(1800, 500, 0, 100);
            createSkeleton(500, 630, 0, 100);
            createSkeleton(600, 630, 0, 100);
            createSkeleton(700, 630, 0, 100);
            createSkeleton(800, 630, 0, 100);
            createSkeleton(550, 630, 0, 100);
            createSkeleton(650, 630, 0, 100);
            createSkeleton(750, 630, 0, 100);
            createSkeleton(850, 630, 0, 100);
            createSkeleton(1500, 630, 0, 100);
            createSkeleton(1600, 630, 0, 100);
            createSkeleton(1700, 630, 0, 100);
            createSkeleton(1800, 630, 0, 100);
            createSkeleton(1550, 630, 0, 100);
            createSkeleton(1650, 630, 0, 100);
            createSkeleton(1750, 630, 0, 100);
            createSkeleton(1850, 630, 0, 100);
            //createWraiths
            createWraith(500, 600, 0, 200, 100);
            createWraith(600, 600, 0, 200, 100);
            createWraith(1250, 470, 0, 200, 100);
            createWraith(1200, 470, 0, 200, 100);
            createWraith(1350, 470, 0, 200, 100);
            createWraith(1300, 470, 0, 200, 100);
            //create Keys
            createKey(1000, 300);
            createKey(1200, 200);
            createKey(1470, 200);
            createKey(1500, 600);
            createKey(1500, 650);
            createKey(1700, 600);
            createKey(1700, 650);
            createKey(1770, 200);
            createKey(1550, 470);
            createKey(1600, 470);
            createKey(1650, 470);
            createKey(1700, 470);

            //createDoor
            createDoor(1000, 500);
            //key events
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            //timer
            Timer timer = new Timer();

            timer.Interval = 1;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        //key events
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D) 
            {
                right = 0;
            }
            if(e.KeyCode == Keys.A) 
            {
                left = 0;
                
            } 

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (adventurer.attacked == 0)
            {
                if(onLadder==0)
                {
                    if (e.KeyCode == Keys.W && jumpCt < 1)
                    {
                        if (adventurer.dst.X > doors[0].dst.X && adventurer.dst.X <doors[0].dst.X+ doors[0].dst.Width && adventurer.dst.Y > doors[0].dst.Y && adventurer.dst.Y < doors[0].dst.Y + doors[0].dst.Width && doors[0].cnd == 1) 
                        {
                            gameOver = 2;
                        } 
                        jumpCt++;
                        up = 1;
                        adventurer.attack = 0;
                    }
                    if (e.KeyCode == Keys.A)
                    {
                        left = 1;
                        adventurer.side = 1;
                    }
                    if (e.KeyCode == Keys.D)
                    {
                        right = 1;
                        adventurer.side = 0;
                    }
                    if (e.KeyCode == Keys.S)
                    {
                        down = 1;
                    }

                    if (e.KeyCode == Keys.Space && laserCnd == 0 && adventurer.side == 1)
                    {
                        createLaserBwd(adventurer.dst.X, adventurer.dst.Y - 10);
                        laserCnd = 1;
                    }
                    if (e.KeyCode == Keys.Space && laserCnd == 0 && adventurer.side == 0)
                    {
                        laserCnd = 1;
                        createLaserFwd(adventurer.dst.X, adventurer.dst.Y - 10);
                    }
                    if (e.KeyCode == Keys.E && adventurer.attack == 0 && attackCd == 0)
                    {
                        adventurer.attack = 1;
                    }
                    if (e.KeyCode == Keys.Q && shooterCnd == -1)
                    {
                        shooterCnd = 0;
                    }
                    if (e.KeyCode == Keys.Y)
                    {
                        createWraith(500, 600, 0, 200, 100);
                        createWraith(600, 600, 0, 200, 100);
                    }
                }
                if (ladderHit(adventurer.dst.X, adventurer.dst.Y) != -1)
                {
                    if (e.KeyCode == Keys.W && onLadder == 0)
                    {
                        onLadder = 1;
                        left = 0;
                        right = 0;
                    }
                    if (onLadder == 1)
                    {
                        if (e.KeyCode == Keys.W && onLadder == 1)
                        {
                            if (adventurer.dst.Y < ladders[ladderHit(adventurer.dst.X, adventurer.dst.Y)].dst.Y - 80)
                            {
                                adventurer.dst.Y -= 25;
                            }
                            else
                            {
                                adventurer.dst.Y -= 1;
                            }
                            climb++;
                            if (climb == 20)
                            {
                                climb = 0;
                            }
                            adventurer.attack = 0;
                        }
                        if (e.KeyCode == Keys.S)
                        {
                            adventurer.dst.Y++;
                            climb++;
                            if (climb == 20)
                            {
                                climb = 0;
                            }
                        }
                        if (e.KeyCode == Keys.D)
                        {
                            adventurer.dst.X += 25;
                            onLadder = 0;
                        }
                        if (e.KeyCode == Keys.A)
                        {
                            adventurer.dst.X -= 25;
                            onLadder = 0;
                        }
                    }
                }
            }
        }
        //timer 
        private void Timer_Tick(object sender, EventArgs e)
        {
            drawDouble(this.CreateGraphics());
            if (gameOver == 0)
            {
                gravity = 1;
                getKey();
                if (allKeys() == 1) 
                {
                    doors[0].cnd = 1;
                }
                for(int i = 0; i < keys.Count; i++) 
                {
                    if (keys[i].cnd == 1) 
                    {
                        keys[i].curr++;
                        if (keys[i].curr == 24) 
                        {
                            keys[i].curr = 0;
                        }
                    }
                }
                if (attackCd > 0)
                {
                    attackCd--;
                }
                if (adventurer.attacked > 0 && interval % 3 == 0)
                {
                    adventurer.attacked++;
                    if (adventurer.side == 1)
                    {
                        adventurer.dst.X -= 40;
                    }
                    else
                    {
                        adventurer.dst.X += 40;
                    }
                }
                if (adventurer.attacked == 2)
                {
                    adventurer.attacked = 0;
                }
                for (int i = 0; i < wraiths.Count; i++)
                {

                    if (wraiths[i].aggro == 0)
                    {
                        if (wraiths[i].move == wraiths[i].max)
                        {
                            wraiths[i].cnd = 1;
                            wraiths[i].side = 0;
                        }
                        if (wraiths[i].move == 0)
                        {
                            wraiths[i].side = 1;
                            wraiths[i].cnd = 0;
                        }
                        if (wraiths[i].cnd == 0)
                        {
                            wraiths[i].move++;
                            wraiths[i].dst.X++;
                        }
                        if (wraiths[i].cnd == 1)
                        {
                            wraiths[i].move--;
                            wraiths[i].dst.X--;
                        }
                        if (interval % 2 == 0)
                            wraiths[i].idle++;
                        if (wraiths[i].idle == 6)
                        {
                            wraiths[i].idle = 0;
                        }
                    }
                    if (wraiths[i].aggro == 1)
                    {
                        if (wraiths[i].dst.X > adventurer.dst.X)
                        {
                            wraiths[i].dst.X--;
                            wraiths[i].side = 0;
                        }
                        if (wraiths[i].dst.X < adventurer.dst.X)
                        {
                            wraiths[i].dst.X++;
                            wraiths[i].side = 1;
                        }
                        if (wraiths[i].dst.Y > adventurer.dst.Y)
                        {
                            wraiths[i].dst.Y--;
                        }
                        if (wraiths[i].dst.Y < adventurer.dst.Y)
                        {
                            wraiths[i].dst.Y++;
                        }
                        if (wraithAttack(i) == -1)
                        {
                            if (interval % 3 == 0)
                            {
                                if (wraiths[i].rev == 0)
                                    wraiths[i].agroAnim++;
                                if (wraiths[i].rev == 1)
                                    wraiths[i].agroAnim--;
                            }

                            if (wraiths[i].agroAnim == 6)
                            {
                                wraiths[i].rev = 1;
                            }

                            if (wraiths[i].agroAnim == 0)
                            {
                                wraiths[i].rev = 0;
                            }
                        }
                        else
                        {
                            if (wraiths[i].attack == 0)
                            {
                                wraiths[i].attacked = 1;
                            }
                        }
                    }
                    if (wraiths[i].attacked == 1 && interval % 3 == 0)
                    {
                        wraiths[i].attack++;
                    }
                    if (wraiths[i].attack == 4)
                    {
                        wraiths[i].attacked = 0;
                        adventurer.hp--;

                        adventurer.attacked = 1;
                        if (wraiths[i].side == 0)
                        {
                            adventurer.side = 1;
                        }
                        else
                        {
                            adventurer.side = 0;
                        }
                        wraiths[i].attack = 0;
                    }
                    if (adventurer.dst.X < wraiths[i].dst.X + wraiths[i].dst.Width + wraiths[i].range && adventurer.dst.X > wraiths[i].dst.X - wraiths[i].range && adventurer.dst.Y < wraiths[i].dst.Y + wraiths[i].dst.Height + wraiths[i].range && adventurer.dst.Y > wraiths[i].dst.Y - wraiths[i].range)
                    {
                        wraiths[i].aggro = 1;
                    }
                    if (wraiths[i].hit == 1 && interval % 4 == 0)
                    {
                        wraiths[i].hit = 0;
                        wraiths[i].hp--;
                        if (wraiths[i].hp == 0)
                        {
                            wraiths.RemoveAt(i);
                        }
                    }
                }
                for(int i = 0; i < skeletons.Count; i++) 
                {
                    if (skeletons[i].dead == 0)
                    {
                        if (skeletons[i].hit == 0)
                        {
                            if (skeletonAttack(i) == -1 && skeletons[i].attack == 0)
                            {
                                if (skeletons[i].move == skeletons[i].max)
                                {
                                    skeletons[i].cnd = 0;
                                    skeletons[i].side = 1;
                                }
                                if (skeletons[i].move == 0)
                                {
                                    skeletons[i].cnd = 1;
                                    skeletons[i].side = 0;
                                }
                                if (skeletons[i].cnd == 1)
                                {
                                    skeletons[i].move++;
                                    skeletons[i].dst.X++;
                                }
                                if (skeletons[i].cnd == 0)
                                {
                                    skeletons[i].move--;
                                    skeletons[i].dst.X--;
                                }
                                if (interval % 2 == 0)
                                {
                                    skeletons[i].idle++;
                                }
                                if (skeletons[i].idle == 4)
                                {
                                    skeletons[i].idle = 0;
                                }
                            }
                            else
                            {
                                if (skeletons[i].attacked == 0)
                                {
                                    skeletons[i].attacked = 1;
                                }

                            }
                            if (skeletons[i].attacked == 1 && interval % 3 == 0)
                            {
                                skeletons[i].attack++;
                            }
                            if (skeletons[i].attack == 8)
                            {
                                skeletons[i].attacked = 0;
                                adventurer.hp--;
                                adventurer.attacked = 1;
                                if (skeletons[i].side == 0)
                                {
                                    adventurer.side = 1;
                                }
                                else
                                {
                                    adventurer.side = 0;
                                }
                                skeletons[i].attack = 0;
                            }
                        }
                        if (skeletons[i].hit == 1 && interval % 4 == 0)
                        {
                            skeletons[i].hit = 0;
                            skeletons[i].hp--;
                            if (skeletons[i].hp == 0)
                            {
                                skeletons[i].dead = 1;
                            }
                        }
                    }
                    else 
                    {
                        skeletons[i].dead++;
                        if (skeletons[i].dead == 4) 
                        {
                            skeletons.RemoveAt(i);
                        }
                    }

                }
                
                for (int i = 0; i < bullets.Count; i++)
                {

                    if (bullets[i].dir == 0)
                    {
                        bullets[i].dst.X += 2;

                    }
                    if (bullets[i].dir == 1)
                    {
                        bullets[i].dst.X -= 2;
                    }
                    if (bullets[i].cnd == 0 && interval % 3 == 0)
                    {
                        bullets[i].curr++;
                    }
                    if (bullets[i].cnd == 1 && interval % 3 == 0)
                    {
                        bullets[i].curr--;
                    }
                    if (bullets[i].curr == 2)
                    {
                        bullets[i].cnd = 1;
                    }
                    if (bullets[i].curr == 0)
                    {
                        bullets[i].cnd = 0;
                    }
                    bullets[i].lifetime--;
                    if (bullets[i].lifetime == 0)
                    {
                        bullets.RemoveAt(i);
                    }
                    else
                    {
                        int cn = 0;
                        for (int k = 0; k < tiles.Count; k++)
                        {
                            if (bullets[i].dst.X > tiles[k].dst.X && bullets[i].dst.X < tiles[k].dst.X + tiles[k].dst.Width && bullets[i].dst.Y > tiles[k].dst.Y - 30 && bullets[i].dst.Y < tiles[k].dst.Y + tiles[k].dst.Height)
                            {
                                bullets.RemoveAt(i);
                                cn = 1;
                                break;
                            }
                        }
                        if (cn == 0)
                        {
                            int n = wraithHit(i);
                            if (n != -1)
                            {
                                wraiths[n].hit = 1;
                                bullets.RemoveAt(i);
                                cn = 1;
                            }
                        }
                        if (cn == 0)
                        {
                            int n = skeletonHit(i);
                            if (n != -1)
                            {
                                skeletons[n].hit = 1;
                                bullets.RemoveAt(i);
                            }
                        }
                    }


                }
                if (tileHitXY(adventurer.dst.X, adventurer.dst.Y) != -1 || onLadder == 1 || elevatorHit(adventurer.dst.X, adventurer.dst.Y) != -1)
                {
                    if (up == 2)
                    {
                        up = 0;
                    }
                    gravity = 0;
                    airbourne = 0;
                    jumpCt = 0;
                }
                if (ladderHit(adventurer.dst.X, adventurer.dst.Y) == -1)
                {
                    onLadder = 0;
                    climb = 0;
                }
                if (elevatorHit(adventurer.dst.X, adventurer.dst.Y) != -1 && elevators[elevatorHit(adventurer.dst.X, adventurer.dst.Y)].cnd == 1)
                {
                    adventurer.dst.Y -= 1;
                }
                if (elevatorHit(adventurer.dst.X, adventurer.dst.Y) != -1 && elevators[elevatorHit(adventurer.dst.X, adventurer.dst.Y)].cnd == 0)
                {
                    adventurer.dst.Y += 1;
                }
                if (adventurer.attack > 0)
                {
                    adventurer.attack++;

                }
                if (adventurer.attack > 9)
                {
                    adventurer.attack = 0;
                    attackCd = 5;
                }
                if (gravity == 1)
                {
                    adventurer.dst.Y += 5;
                    airbourne = 1;
                }
                if (laserCnd != 0)
                {
                    if (interval % 2 == 0)
                    {
                        laserCnd++;
                        if (laserCnd > 5)
                        {
                            beam.curr++;
                            wraithLaserHit();
                            skeletonLaserHit();
                        }
                        if (laserCnd == 12)
                        {
                            laserCnd = 0;
                            beam.curr = 0;
                        }
                    }
                }
                if (shooterCnd != -1)
                {
                    if (interval % 2 == 0)
                    {
                        shooterCnd++;
                        if (shooterCnd == 4)
                        {
                            createBullet();
                        }
                        if (shooterCnd > 6)
                        {
                            shooterCnd = -1;
                        }
                    }
                }
                if (up == 1)
                {
                    if (upward < 5 && tileHitX(adventurer.dst.X, adventurer.dst.Y - 50) == -1)
                    {

                        upward += 1;
                        adventurer.dst.Y -= 14;
                        if (adventurer.jump < 5)
                        {
                            adventurer.jump++;
                        }

                    }
                    else
                    {
                        upward = 0;
                        adventurer.jump = 0;
                        up = 2;
                    }
                }
                if (left == 0 && right == 0)
                {
                    move = 0;
                }
                else
                {
                    adventurer.idle = 0;
                }
                if (move == 0)
                {
                    if (interval % 8 == 0)
                    {
                        adventurer.idle++;
                        if (adventurer.idle == 10)
                        {
                            adventurer.idle = 0;
                        }
                    }
                }
                if (left == 1)
                {
                    move = 1;
                    if (tileHitX(adventurer.dst.X - 3, adventurer.dst.Y) == -1)
                    {
                        if (adventurer.dst.X > 40)
                        {
                            adventurer.dst.X -= speed;
                        }
                        if (tiles[0].dst.X < 0)
                        {
                            move_objects(2);
                        }
                    }

                    if (adventurer.curr < 9)
                    {
                        adventurer.curr++;
                    }
                    else
                    {
                        adventurer.curr = 0;
                    }
                }
                if (right == 1)
                {
                    move = 1;
                    if (tileHitX(adventurer.dst.X + 1, adventurer.dst.Y) == -1)
                    {
                        if (adventurer.dst.X < Screen.GetWorkingArea(this).Width / 2)
                        {
                            adventurer.dst.X += speed;
                        }
                        else
                        {
                            move_objects(-2);
                        }

                    }

                    if (adventurer.curr < 9)
                    {
                        adventurer.curr++;
                    }
                    else
                    {
                        adventurer.curr = 0;
                    }
                }
                for (int i = 0; i < elevators.Count; i++)
                {
                    if (elevators[i].cnd == 0)
                    {
                        elevators[i].dst.Y++;
                    }
                    if (elevators[i].cnd == 1)
                    {
                        elevators[i].dst.Y--;
                    }
                    if (elevators[i].cnd == 0)
                    {
                        elevators[i].move++;
                    }
                    else
                    {
                        elevators[i].move--;
                    }
                    if (elevators[i].move == elevators[i].max)
                    {
                        elevators[i].cnd = 1;
                    }
                    if (elevators[i].move == elevators[i].min)
                    {
                        elevators[i].cnd = 0;
                    }
                }
                interval++;
                if (adventurer.hp <= 0)
                {
                    gameOver = 1;
                }
            }
        }
        //general functions 
        public void move_objects(int amount) 
        {
            for (int i = 0; i < bg.Count; i++)
            {
                bg[i].dst.X += amount;
            }
            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].dst.X += amount;
            }
            for(int i=0;i<elevators.Count;i++) 
            {
                elevators[i].dst.X += amount;
            }
            for(int i = 0; i < ladders.Count; i++) 
            {
                ladders[i].dst.X += amount;
            }
            for(int i = 0; i < wraiths.Count; i++) 
            {
                wraiths[i].dst.X += amount;
            }
            for (int i = 0; i < skeletons.Count; i++)
            {
                skeletons[i].dst.X += amount;
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].dst.X += amount;
            }
            for(int i = 0; i < keys.Count; i++) 
            {
                keys[i].dst.X += amount;
            }
            for(int i=0; i < doors.Count; i++) 
            {
                doors[i].dst.X += amount;
            }
        }

        public int tileHitXY(int x,int y)
        {
            int r = -1;
            for (int i = 0; i < tiles.Count; i++)
            {
                if (y > tiles[i].dst.Y - 83 && x > tiles[i].dst.X - 74 && x < tiles[i].dst.X + tiles[i].dst.Width - 55 && adventurer.dst.Y < tiles[i].dst.Y + tiles[i].dst.Height - 50 ) 
                {
                    return i;
                }
            }
            
            return r;

        }
        public int elevatorHit(int x, int y) 
        {
            int r = -1;
            for (int i = 0; i < elevators.Count; i++)
            {
                if (y > elevators[i].dst.Y -83  && x > elevators[i].dst.X-74   && x < elevators[i].dst.X + elevators[i].dst.Width -50   && adventurer.dst.Y < elevators[i].dst.Y -60 )
                {
                    return i;
                }
            }
            return r;
        }
        public int ladderHit(int x, int y)
        {
            int r = -1;
            for (int i = 0; i < ladders.Count; i++)
            {
                if (y > ladders[i].dst.Y - 83 && x > ladders[i].dst.X - 74 && x < ladders[i].dst.X + ladders[i].dst.Width - 50 && adventurer.dst.Y < ladders[i].dst.Y + ladders[i].dst.Width+20)
                {
                    return i;
                }
            }
            return r;
        }
        public int tileHitX(int x,int y)
        {
            int r = -1;
            for (int i = 0; i < tiles.Count; i++)
            {
                if (y > tiles[i].dst.Y - 70 && x > tiles[i].dst.X - 70  && x < tiles[i].dst.X + tiles[i].dst.Width - 60  && adventurer.dst.Y < tiles[i].dst.Y + tiles[i].dst.Height-10)
                {
                    return i;
                }
            }
            return r;

        }
        public int tileHitY(int y)
        {
            int r = -1;
            for (int i = 0; i < tiles.Count; i++)
            {
                if (y > tiles[i].dst.Y - 83 && y < tiles[i].dst.Y + tiles[i].dst.Height-50 )
                {
                   return i;
                }
            }
            return r;

        }
        public int wraithAttack(int i) 
        {
            int r = -1;
            if (adventurer.dst.Y >= wraiths[i].dst.Y && adventurer.dst.X > wraiths[i].dst.X-50 && adventurer.dst.X < wraiths[i].dst.X + wraiths[i].dst.Width -70  && adventurer.dst.Y < wraiths[i].dst.Y + wraiths[i].dst.Height-50)
            {
                return i;
            }
            return r;
        }
        
        public int skeletonAttack(int i)
        {
            int r = -1;
            if (adventurer.dst.Y >= skeletons[i].dst.Y -30 && adventurer.dst.X > skeletons[i].dst.X-30  && adventurer.dst.X < skeletons[i].dst.X +  skeletons[i].dst.Width - 70  && adventurer.dst.Y < skeletons[i].dst.Y + skeletons[i].dst.Height )
            {
                return i;
            }
            return r;
        }
        public int wraithHit(int i) 
        {
            for(int k = 0; k < wraiths.Count; k++) 
            {
                if (wraiths[k].dst.X > bullets[i].dst.X -70 && wraiths[k].dst.X < bullets[i].dst.X + bullets[i].dst.Width && wraiths[k].dst.Y > bullets[i].dst.Y-40 && wraiths[k].dst.Y < bullets[i].dst.Y +60) 
                {
                    return k;
                }
            }
            return -1;
        }
        public int getKey() 
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (adventurer.dst.X > keys[i].dst.X - 60 && adventurer.dst.X < keys[i].dst.X + keys[i].dst.Width -50 && adventurer.dst.Y > keys[i].dst.Y - 30 && adventurer.dst.Y < keys[i].dst.Y + keys[i].dst.Height - 30) 
                {
                    keys[i].cnd = 0;
                }
            }
            return -1;
        }
        
        public int allKeys()
        {
            int ct = 0;
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i].cnd == 0) 
                {
                    ct++;
                }
            }
            if (ct == keys.Count) 
            {
                return 1;
            }
            return -1;
        }
        public int skeletonHit(int i)
        {
            for (int k = 0; k < skeletons.Count; k++)
            {
                if (skeletons[k].dst.X + 50  > bullets[i].dst.X  && skeletons[k].dst.X + 50 < bullets[i].dst.X + bullets[i].dst.Width && skeletons[k].dst.Y > bullets[i].dst.Y - 40 && skeletons[k].dst.Y < bullets[i].dst.Y + 60)
                {
                    return k;
                }
            }
            return -1;
        }
        public int wraithLaserHit()
        {
            for (int k = 0; k < wraiths.Count; k++)
            {
                if (wraiths[k].dst.X > beam.dst.X - 70 && wraiths[k].dst.X < beam.dst.X + beam.dst.Width && wraiths[k].dst.Y > beam.dst.Y - 40 && wraiths[k].dst.Y < beam.dst.Y + 60)
                {
                    wraiths[k].hit=1;
                }
            }   
            return -1;
        }
        public int skeletonLaserHit()
        {
            for (int k = 0; k < skeletons.Count; k++)
            {
                if (skeletons[k].dst.X > beam.dst.X - 70 && skeletons[k].dst.X < beam.dst.X + beam.dst.Width && skeletons[k].dst.Y > beam.dst.Y - 40 && skeletons[k].dst.Y < beam.dst.Y + 60)
                {
                    skeletons[k].hit = 1;
                }
            }
            return -1;
        }
        //objects creation functions
        public void createAdventurer() 
        {
            adventurer = new Adventurer();
            adventurer.dst.X = 10;
            adventurer.dst.Y = 545;
            adventurer.hp = 4;
            adventurer.src.X = 0;
            adventurer.src.Y = 0;

            for(int i = 0; i < 10; i++) 
            {
                for (int k = 0; k < Moveframes; k++)
                {
                    adventurer.FwdMove.Add(new Bitmap(@"..\..\project assets\Run\Run_" + i + ".png"));

                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < Moveframes; k++)
                {
                    adventurer.BwdMove.Add(new Bitmap(@"..\..\project assets\Bwd_Run\Run_" + i + ".png"));
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < Moveframes; k++)
                {
                    adventurer.FwdIdle.Add(new Bitmap(@"..\..\project assets\idle\idle_" + i + ".png"));

                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < Moveframes; k++)
                {
                    adventurer.BwdIdle.Add(new Bitmap(@"..\..\project assets\Bwd_Idle\idle_" + i + ".png"));
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 2; k++)
                {
                    adventurer.FwdJump.Add(new Bitmap(@"..\..\project assets\jump\Jump_" + i + ".png"));

                }
            }
            for (int i = 1; i < 5; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    adventurer.climb.Add(new Bitmap(@"..\..\project assets\climb2\climb" + i + ".png"));

                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 2; k++)
                {
                    adventurer.BwdJump.Add(new Bitmap(@"..\..\project assets\Bwd_Jump\Jump_" + i + ".png"));
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < Moveframes; k++)
                {
                    adventurer.FwdAttack.Add(new Bitmap(@"..\..\project assets\attack\_AttackComboNoMovement_" + i + ".png"));

                }

            }
            for (int i = 0; i < 8; i++)
            {
                beam.Fwdimg.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\laserBeam\LaserGun_Burn._"+i+".png"));
            }
            for (int i = 0; i < 8; i++)
            {
                beam.Bwdimg.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\BwdLaserBeam\LaserGun_Burn._" + i + ".png"));
            }
            for (int i = 0; i <10; i++)
            {
                for (int k = 0; k < Moveframes; k++)
                {
                    adventurer.BwdAttack.Add(new Bitmap(@"..\..\project assets\Bwd_attack\_AttackComboNoMovement_" + i + ".png"));
                }
            }
            for(int i = 0; i < 7; i++) 
            {
                iz.img.Add(new Bitmap(@"..\..\project assets\bullet\iz403_0" + i + ".png"));
            }
            for (int i = 0; i < 7; i++)
            {
                iz.fwdimg.Add(new Bitmap(@"..\..\project assets\bulletFwd\iz403_0" + i + ".png"));
            }
            for(int i = 0; i < 10; i++) 
            {
                adventurer.Death.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\death\DeathNoMovement_" + i + ".png"));
            }
            for (int i = 0; i < 10; i++)
            {
                adventurer.BwdDeath.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\BwdDeath\DeathNoMovement_" + i + ".png"));
            }
            adventurer.src.Width  = adventurer.FwdAttack[adventurer.attack].Width;
            adventurer.src.Height = adventurer.FwdAttack[adventurer.attack].Height;
            adventurer.dst.Width  = adventurer.FwdAttack[adventurer.attack].Width;
            adventurer.dst.Height = adventurer.FwdAttack[adventurer.attack].Height;
        }
        
        public void createLaserFwd(int x, int y) 
        {
            if (laser == null)
            {
                laser = new Laser();
            }
            laser.dst.X = x;
            laser.dst.Y = y;
            laser.dst.Width = 70;
            laser.dst.Height = 70;
            beam.dst.X = laser.dst.X+1;
            if (laser.Fwdimg.Count != 12)
            {
                for (int i = 0; i < 10; i++)
                {
                    laser.Fwdimg.Add(new Bitmap(@"..\..\project assets\laserFwd\tt402_0" + i + ".png"));
                }
                laser.Fwdimg.Add(new Bitmap(@"..\..\project assets\laserFwd\tt402_10.png"));
                laser.Fwdimg.Add(new Bitmap(@"..\..\project assets\laserFwd\tt402_11.png"));
            }
        }
        public void createLadder(int x , int y ) 
        {
            ladder ladder = new ladder();
            ladder.dst.X = x;
            ladder.dst.Y = y;
            ladder.dst.Width = 30;
            ladder.dst.Height = 120;
            ladder.img = new Bitmap(@"..\..\project assets\ladder.png");
            ladder.src.X = 0; ladder.src.Y = 0;
            ladder.src.Width = ladder.img.Width; ladder.src.Height = ladder.img.Height;

            ladders.Add(ladder);
        }
        public void createLaserBwd(int x, int y)
        {
            if (laser == null)
            {
                laser = new Laser();
            }
            laser.dst.Y = y;
            laser.dst.Width = 70;
            laser.dst.Height = 70;
            beam.dst.X = laser.dst.X + 1;

            if (laser.Bwdimg.Count != 12)
            {
                for (int i = 0; i < 10; i++)
                {
                    laser.Bwdimg.Add(new Bitmap(@"..\..\project assets\laser\tt402_0" + i + ".png"));
                }
                laser.Bwdimg.Add(new Bitmap(@"..\..\project assets\laser\tt402_10.png"));
                laser.Bwdimg.Add(new Bitmap(@"..\..\project assets\laser\tt402_11.png"));
            }
        }
        public void createShooter(int side) 
        {
            iz.dst.X = adventurer.dst.X +10;
            iz.dst.Y = adventurer.dst.Y;
            iz.dst.Width = 70;
            iz.dst.Height = 70;
            iz.src.X=0; iz.src.Y = 0;
            if (side == 0) {
                int x = 0;
                if (shooterCnd == 2) { x = 300; }
                iz.src.Width = iz.fwdimg[shooterCnd].Width+x;
                iz.src.Height = iz.fwdimg[shooterCnd].Height;
            }
            else 
            {
                iz.src.Width = iz.img[shooterCnd].Width;
                iz.src.Height = iz.img[shooterCnd].Height;
            }
        }
        public void createBullet() 
        {
            
            bullet bullet=new bullet();
            if (adventurer.side == 0)
            {
                bullet.dst.X = adventurer.dst.X + 80;
                bullet.dir = 0;
            }
            else 
            {
                bullet.dst.X = adventurer.dst.X - 10;
                bullet.dir = 1;
            }
            for (int i = 0; i < 3; i++)
            {
                bullet.img.Add(new Bitmap(@"..\..\project assets\BwdBullets\bullet"+i+".png"));
                bullet.img[i].MakeTransparent(bullet.img[i].GetPixel(0, 0));
            }
            for (int i = 0; i < 3; i++)
            {
                bullet.revImg.Add(new Bitmap(@"..\..\project assets\Bullets\bullet" + i + ".png"));
                bullet.revImg[i].MakeTransparent(bullet.revImg[i].GetPixel(0, 0));
            }
            bullet.dst.Y = adventurer.dst.Y + 20;
            bullet.dst.Width = 20;
            bullet.dst.Height = 20;
            bullet.src.X = 0;
            bullet.src.Y=0;
            bullet.src.Width = bullet.img[0].Width; bullet.src.Height = bullet.img[0].Height;

            bullet.lifetime = 80;
            bullets.Add(bullet);
        }
        public void createKey(int x,int y) 
        {
            key key = new key();
            key.dst.X = x;
            key.dst.Y = y;
            key.dst.Width = 30;
            key.dst.Height = 30;
            for(int i = 0; i < 24; i++) 
            {
                key.img.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\key\key__" + i + ".png"));
            }
            key.curr = 0;
            key.cnd = 1;
            key.src=new Rectangle(0, 0, key.img[0].Width, key.img[0].Height);
            keys.Add(key);
            
        }
        public void createDoor(int x,int y) 
        {
            door door = new door();
            door.dst.X = x;
            door.dst.Y = y;
            door.dst.Width = 200;
            door.dst.Height =200;
            door.cnd = 0;
            door.src.X = 0;
            door.src.Y = 0;
            door.img.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\door\closed.png"));
            door.img.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\door\open.png"));
            doors.Add(door);

        }
        public void createSkeleton(int x,int y ,int move,int max) 
        {
            skeleton skeleton = new skeleton();
            skeleton.dst.X = x;
            skeleton.dst.Y = y;
            skeleton.dst.Width = 100;
            skeleton.max = max;
            skeleton.dst.Height = 100;
            skeleton.move = move;
            skeleton.attack = 0;
            skeleton.attacked = 0;
            skeleton.side = 0;
            skeleton.death.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\skeletonDeath\Death_" + 0 + ".png"));
            for (int i = 0; i < 4; i++) 
            {
                skeleton.death.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\skeletonDeath\Death_" + i + ".png"));
                skeleton.walk.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\walk\walk_" + i + ".png"));
                skeleton.Hit.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\hit\Take Hit_" + i + ".png"));
                skeleton.walkBwd.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\walkBwd\walk_" + i + ".png"));
                skeleton.HitBwd.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\hitBwd\Take Hit_" + i + ".png"));

            }
            for (int i = 0; i < 8; i++) 
            {
                skeleton.Attack.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\skeletonAttack\Attack_" + i + ".png"));
                skeleton.BwdAttack.Add(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\skeletonAttackBwd\Attack_" + i + ".png"));
            }
            skeletons.Add(skeleton);
        }
        public void createBg()
        {
            for (int i = 0; i < 10; i++)
            {
                Bg b = new Bg();
                //if (i == 0)
                //{
                //    b.dst = new Rectangle(i * 1480, 0, 1490, 700);
                //    b.img = new Bitmap(@"..\..\project assets\Background_Alpha.png");
                //    b.src = new Rectangle(0, 0, b.img.Width, b.img.Height);
                //    bg.Add(b);
                //}
                //else
                {
                    b.dst = new Rectangle(i * 1280, 0, 1290, 700);
                    b.img = new Bitmap(@"..\..\project assets\Background_Omega.png");
                    b.src = new Rectangle(0, 0, b.img.Width, b.img.Height);
                    bg.Add(b);
                }
            }
        }
        public void createTile(int x,int y,int w, int h,int n)
        {
            for (int i = 0; i < n; i++)
            {
                tile tile = new tile();
                tile.dst = new Rectangle(x, y, w, h);
                tile.img = new Bitmap(@"..\..\project assets\tile.png");
                tile.src = new Rectangle(0, 0, tile.img.Width, tile.img.Height);
                x += w;
                tiles.Add(tile);
            }
        }
        public void createVertTile(int x, int y, int w, int h, int n)
        {
            for (int i = 0; i < n; i++)
            {
                tile tile = new tile();
                tile.dst = new Rectangle( x, i * y, w, h);
                tile.img = new Bitmap(@"..\..\project assets\tile.png");
                tile.src = new Rectangle(0, 0, tile.img.Width, tile.img.Height);
                tiles.Add(tile);
            }
        }
        public void createEle(int x,int y,int max ,int min) 
        {
            elevator elevator = new elevator();
            elevator.dst.X = x;
            elevator.dst.Y=y;
            elevator.max = max;
            elevator.min = min;
            elevator.dst.Height = 40;
            elevator.dst.Width = 60;
            elevator.img = new Bitmap(@"..\..\project assets\elevator.png");
            elevator.src=new Rectangle(0,0,elevator.img.Width,elevator.img.Height);
            elevator.move = 0;
            elevator.cnd = 0;
            elevators.Add(elevator);
        }
        public void createWraith(int x,int y,int move,int range,int max) 
        {
            Wraith wraith = new Wraith();
            wraith.dst.X = x;
            wraith.dst.Y = y;
            wraith.dst.Width = 100;
            wraith.max=max;
            wraith.dst.Height = 100;
            wraith.aggro = 0;
            wraith.move = move;
            wraith.attack = 0;
            wraith.range=range;
            wraith.idle = 0;
            wraith.side = 0;
            for (int i = 0; i < 4; i++)
            {
                wraith.Attack.Add(new Bitmap(@"..\..\project assets\BwdAttackWraith\AttackWraith_"+i+".png"));
            }
            for (int i = 0; i < 4; i++)
            {
                wraith.BwdAttack.Add(new Bitmap(@"..\..\project assets\AttackWraith\AttackWraith_"+i+".png"));
            }
            for (int i = 0; i < 7; i++)
            {
                wraith.Agro.Add(new Bitmap(@"..\..\project assets\BwdWraithIdle\Wraith_idle_" + i + ".png"));
            }
            for (int i = 0; i < 7; i++)
            {
                wraith.BwdAgro.Add(new Bitmap(@"..\..\project assets\WraithIdle\Wraith_idle_"+i+".png"));
            }
            for (int i = 0; i < 6; i++)
            {
                wraith.nonAgro.Add(new Bitmap(@"..\..\project assets\BwdNonAgro\nonAgroWraith_"+i+".png"));
            }
            for (int i = 0; i < 6; i++)
            {
                wraith.BwdnonAgro.Add(new Bitmap(@"..\..\project assets\NonAgro\nonAgroWraith_" + i + ".png"));
            }
            for (int i = 0; i < 4; i++)
            {
                wraith.death.Add(new Bitmap(@"..\..\project assets\FwdWraithDeath\Death_Wraith_" + i + ".png"));
            }
            for (int i = 0; i < 4; i++)
            {
                wraith.Bwddeath.Add(new Bitmap(@"..\..\project assets\WraithDeath\Death_Wraith_"+i+".png"));
            }
            wraiths.Add(wraith);
        }
        //draw
        public void draw(Graphics g2)
        {
            if (gameOver == 0)
            {
               
                g2.Clear(BackColor);

                for (int i = 0; i < bg.Count; i++)
                {
                    g2.DrawImage(bg[i].img, bg[i].dst, bg[i].src, GraphicsUnit.Pixel);
                }
                for (int i = 0; i < adventurer.hp; i++)
                {
                    g2.DrawImage(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\idle\Idle_0.png"), i * 20, -20, 100, 100);
                }
                if (laserCnd != 0 && adventurer.side == 1)
                {
                    laser.dst.X = adventurer.dst.X + 10;
                    laser.dst.Y = adventurer.dst.Y;
                    laser.src.Width = laser.Bwdimg[laserCnd].Width;
                    laser.src.Height = laser.Bwdimg[laserCnd].Height;
                    beam.dst.X = laser.dst.X - 400;
                    beam.dst.Y = laser.dst.Y + 15;
                    beam.src.Width = beam.Bwdimg[beam.curr].Width;
                    beam.src.Height = beam.Bwdimg[beam.curr].Height;
                    beam.dst.Width = 500;
                    beam.dst.Height = 50;
                    if (laserCnd > 4)
                    {
                        g2.DrawImage(beam.Bwdimg[beam.curr], beam.dst, beam.src, GraphicsUnit.Pixel);
                    }
                    g2.DrawImage(laser.Bwdimg[laserCnd], laser.dst, laser.src, GraphicsUnit.Pixel);

                }
                for (int i = 0; i < doors.Count; i++)
                {
                    doors[i].src.Width = doors[i].img[doors[i].cnd].Width;
                    doors[i].src.Height = doors[i].img[doors[i].cnd].Height;
                    g2.DrawImage(doors[i].img[doors[i].cnd], doors[i].dst, doors[i].src, GraphicsUnit.Pixel);
                }
                if (laserCnd != 0 && adventurer.side == 0)
                {
                    laser.dst.X = adventurer.dst.X + 10;
                    laser.dst.Y = adventurer.dst.Y;
                    laser.src.Width = laser.Fwdimg[laserCnd].Width;
                    laser.src.Height = laser.Fwdimg[laserCnd].Height;
                    beam.dst.X = laser.dst.X - 30;
                    beam.dst.Y = laser.dst.Y + 15;
                    beam.dst.Height = 50;
                    beam.src.Width = beam.Fwdimg[beam.curr].Width;
                    beam.src.Height = beam.Fwdimg[beam.curr].Height;
                    beam.dst.Width = 500;
                    if (laserCnd > 4)
                    {
                        g2.DrawImage(beam.Fwdimg[beam.curr], beam.dst, beam.src, GraphicsUnit.Pixel);
                    }
                    g2.DrawImage(laser.Fwdimg[laserCnd], laser.dst, laser.src, GraphicsUnit.Pixel);
                }
                if (shooterCnd != -1 && adventurer.side == 1)
                {
                    createShooter(adventurer.side);
                    g2.DrawImage(iz.img[shooterCnd], iz.dst, iz.src, GraphicsUnit.Pixel);
                }
                if (shooterCnd != -1 && adventurer.side == 0)
                {
                    createShooter(adventurer.side);
                    g2.DrawImage(iz.fwdimg[shooterCnd], iz.dst, iz.src, GraphicsUnit.Pixel);
                }
                if (move == 1 && up == 0 && airbourne == 0 && adventurer.attack == 0 && adventurer.attacked == 0)
                {
                    if (adventurer.side == 0 && up == 0)
                    {
                        //adventurer.dst.Width  = adventurer.FwdMove[adventurer.curr].Width ;
                        //adventurer.dst.Height = adventurer.FwdMove[adventurer.curr].Height;

                        g2.DrawImage(adventurer.FwdMove[adventurer.curr], adventurer.dst, adventurer.src, GraphicsUnit.Pixel);
                    }
                    if (adventurer.side == 1 && up == 0)
                    {

                        //adventurer.dst.Width = adventurer.BwdMove[adventurer.curr].Width;
                        //adventurer.dst.Height = adventurer.BwdMove[adventurer.curr].Height ;

                        g2.DrawImage(adventurer.BwdMove[adventurer.curr], adventurer.dst, adventurer.src, GraphicsUnit.Pixel);
                    }
                }

                if ((up == 2 || airbourne == 1 && up != 1) && adventurer.side == 0 && adventurer.attack == 0 && adventurer.attacked == 0)
                {

                    Bitmap jump = new Bitmap(@"..\..\project assets\landing_jumping\JumpFallInbetween_1.png");
                    //adventurer.dst.Width = jump.Width ;
                    //adventurer.dst.Height = jump.Height ;
                    g2.DrawImage(jump, adventurer.dst, adventurer.src, GraphicsUnit.Pixel);
                }
                if ((up == 2 || airbourne == 1 && up != 1) && adventurer.side == 1 && adventurer.attack == 0 && adventurer.attacked == 0)
                {

                    Bitmap jump = new Bitmap(@"..\..\project assets\Bwd_LandingJumping\JumpFallInbetween_1.png");
                    //adventurer.dst.Width = jump.Width;
                    //adventurer.dst.Height = jump.Height;
                    g2.DrawImage(jump, adventurer.dst, adventurer.src, GraphicsUnit.Pixel);
                }
                if ((up == 1) && adventurer.side == 0)
                {
                    //adventurer.dst.Width  = adventurer.FwdJump[adventurer.jump].Width;
                    //adventurer.dst.Height = adventurer.FwdJump[adventurer.jump].Height ;

                    g2.DrawImage(adventurer.FwdJump[adventurer.jump], adventurer.dst, adventurer.src, GraphicsUnit.Pixel);
                }
                if ((up == 1) && adventurer.side == 1)
                {
                    //adventurer.dst.Width  = adventurer.BwdJump[adventurer.jump].Width ;
                    //adventurer.dst.Height = adventurer.BwdJump[adventurer.jump].Height ;

                    g2.DrawImage(adventurer.BwdJump[adventurer.jump], adventurer.dst, adventurer.src, GraphicsUnit.Pixel);
                }
                for (int i = 0; i < tiles.Count; i++)
                {
                    g2.DrawImage(tiles[i].img, tiles[i].dst, tiles[i].src, GraphicsUnit.Pixel);
                }
                for (int i = 0; i < ladders.Count; i++)
                {
                    g2.DrawImage(ladders[i].img, ladders[i].dst, ladders[i].src, GraphicsUnit.Pixel);
                }
                if (move == 0 && adventurer.side == 0 && airbourne == 0 && adventurer.attack == 0 && onLadder == 0 && adventurer.attacked == 0)
                {
                    //adventurer.dst.Width  = adventurer.FwdIdle[adventurer.idle].Width;
                    //adventurer.dst.Height = adventurer.FwdIdle[adventurer.idle].Height;
                    g2.DrawImage(adventurer.FwdIdle[adventurer.idle], adventurer.dst, adventurer.src, GraphicsUnit.Pixel);
                }
                if (move == 0 && adventurer.side == 1 && airbourne == 0 && adventurer.attack == 0 && onLadder == 0 && adventurer.attacked == 0)
                {
                    //adventurer.dst.Width  = adventurer.BwdIdle[adventurer.idle].Width;
                    //adventurer.dst.Height = adventurer.BwdIdle[adventurer.idle].Height;

                    g2.DrawImage(adventurer.BwdIdle[adventurer.idle], adventurer.dst, adventurer.src, GraphicsUnit.Pixel);
                }
                if (adventurer.attack > 0 && adventurer.side == 0 && adventurer.attacked == 0)
                {
                    //adventurer.dst.Width  = adventurer.FwdAttack[adventurer.attack].Width;
                    //adventurer.dst.Height = adventurer.FwdAttack[adventurer.attack].Height;
                    g2.DrawImage(adventurer.FwdAttack[adventurer.attack], adventurer.dst, adventurer.src, GraphicsUnit.Pixel);
                }
                if (adventurer.attack > 0 && adventurer.side == 1 && adventurer.attacked == 0)
                {
                    //adventurer.dst.Width  = adventurer.BwdAttack[adventurer.attack].Width;
                    //adventurer.dst.Height = adventurer.BwdAttack[adventurer.attack].Height;
                    g2.DrawImage(adventurer.BwdAttack[adventurer.attack], adventurer.dst, adventurer.src, GraphicsUnit.Pixel);
                }
                for (int i = 0; i < elevators.Count; i++)
                {
                    g2.DrawImage(elevators[i].img, elevators[i].dst, elevators[i].src, GraphicsUnit.Pixel);
                }
                if (onLadder == 1)
                {
                    adventurer.dst.X += 20;
                    adventurer.dst.Y += 43;
                    adventurer.dst.Width += 30;
                    adventurer.dst.Width += 30;
                    g2.DrawImage(adventurer.climb[climb], adventurer.dst, adventurer.src, GraphicsUnit.Pixel);
                    adventurer.dst.Width -= 30;
                    adventurer.dst.Width -= 30;
                    adventurer.dst.X -= 20;
                    adventurer.dst.Y -= 43;
                }
                if (adventurer.attacked == 1 && adventurer.side == 0)
                {
                    g2.DrawImage(adventurer.Death[0], adventurer.dst, adventurer.src, GraphicsUnit.Pixel);
                }
                if (adventurer.attacked == 1 && adventurer.side == 1)
                {
                    g2.DrawImage(adventurer.BwdDeath[0], adventurer.dst, adventurer.src, GraphicsUnit.Pixel);
                }

                for (int i = 0; i < wraiths.Count; i++)
                {
                    if (wraiths[i].side == 0)
                    {
                        if (wraiths[i].hit == 0)
                        {
                            if (wraiths[i].attack == 0)
                            {
                                if (wraiths[i].aggro == 0)
                                {
                                    wraiths[i].src.X = 0;
                                    wraiths[i].src.Y = 0;
                                    wraiths[i].src.Width = wraiths[i].BwdnonAgro[wraiths[i].idle].Width;
                                    wraiths[i].src.Height = wraiths[i].BwdnonAgro[wraiths[i].idle].Height;
                                    g2.DrawImage(wraiths[i].BwdnonAgro[wraiths[i].idle], wraiths[i].dst, wraiths[i].src, GraphicsUnit.Pixel);
                                }
                                if (wraiths[i].aggro == 1)
                                {
                                    wraiths[i].src.X = 0;
                                    wraiths[i].src.Y = 0;
                                    wraiths[i].src.Width = wraiths[i].BwdAgro[wraiths[i].agroAnim].Width;
                                    wraiths[i].src.Height = wraiths[i].BwdAgro[wraiths[i].agroAnim].Height;
                                    g2.DrawImage(wraiths[i].BwdAgro[wraiths[i].agroAnim], wraiths[i].dst, wraiths[i].src, GraphicsUnit.Pixel);
                                }
                            }
                            else
                            {
                                wraiths[i].src.X = 0;
                                wraiths[i].src.Y = 0;
                                wraiths[i].src.Width = wraiths[i].BwdAttack[wraiths[i].attack].Width;
                                wraiths[i].src.Height = wraiths[i].BwdAttack[wraiths[i].attack].Height;
                                g2.DrawImage(wraiths[i].BwdAttack[wraiths[i].attack], wraiths[i].dst, wraiths[i].src, GraphicsUnit.Pixel);
                            }
                        }
                        else
                        {
                            wraiths[i].src.X = 0;
                            wraiths[i].src.Y = 0;
                            wraiths[i].src.Width = wraiths[i].Bwddeath[2].Width;
                            wraiths[i].src.Height = wraiths[i].Bwddeath[2].Height;
                            g2.DrawImage(wraiths[i].Bwddeath[2], wraiths[i].dst, wraiths[i].src, GraphicsUnit.Pixel);
                        }

                    }
                    else
                    {
                        if (wraiths[i].hit == 0)
                        {
                            if (wraiths[i].attack == 0)
                            {
                                wraiths[i].src.X = 0;
                                wraiths[i].src.Y = 0;
                                if (wraiths[i].aggro == 0)
                                {
                                    wraiths[i].src.Width = wraiths[i].nonAgro[wraiths[i].idle].Width;
                                    wraiths[i].src.Height = wraiths[i].nonAgro[wraiths[i].idle].Height;
                                    g2.DrawImage(wraiths[i].nonAgro[wraiths[i].idle], wraiths[i].dst, wraiths[i].src, GraphicsUnit.Pixel);
                                }
                                if (wraiths[i].aggro == 1)
                                {
                                    wraiths[i].src.X = 0;
                                    wraiths[i].src.Y = 0;
                                    wraiths[i].src.Width = wraiths[i].Agro[wraiths[i].agroAnim].Width;
                                    wraiths[i].src.Height = wraiths[i].Agro[wraiths[i].agroAnim].Height;
                                    g2.DrawImage(wraiths[i].Agro[wraiths[i].agroAnim], wraiths[i].dst, wraiths[i].src, GraphicsUnit.Pixel);
                                }
                            }
                            else
                            {
                                wraiths[i].src.X = 0;
                                wraiths[i].src.Y = 0;
                                wraiths[i].src.Width = wraiths[i].Attack[wraiths[i].attack].Width;
                                wraiths[i].src.Height = wraiths[i].Attack[wraiths[i].attack].Height;
                                g2.DrawImage(wraiths[i].Attack[wraiths[i].attack], wraiths[i].dst, wraiths[i].src, GraphicsUnit.Pixel);
                            }

                        }
                        else
                        {
                            wraiths[i].src.X = 0;
                            wraiths[i].src.Y = 0;
                            wraiths[i].src.Width = wraiths[i].death[2].Width;
                            wraiths[i].src.Height = wraiths[i].death[2].Height;
                            g2.DrawImage(wraiths[i].death[2], wraiths[i].dst, wraiths[i].src, GraphicsUnit.Pixel);
                        }
                    }

                }
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].dir == 1)
                    {
                        g2.DrawImage(bullets[i].img[bullets[i].curr], bullets[i].dst, bullets[i].src, GraphicsUnit.Pixel);
                    }
                    else
                    {
                        g2.DrawImage(bullets[i].revImg[bullets[i].curr], bullets[i].dst, bullets[i].src, GraphicsUnit.Pixel);
                    }
                }
                for (int i = 0; i < skeletons.Count; i++)
                {

                    skeletons[i].src.X = 0;
                    skeletons[i].src.Y = 0;
                    if (skeletons[i].dead == 0)
                    {
                        if (skeletons[i].side == 0)
                        {
                            if (skeletons[i].attacked == 0 && skeletons[i].hit == 0)
                            {
                                skeletons[i].src.Width = skeletons[i].walk[skeletons[i].idle].Width;
                                skeletons[i].src.Height = skeletons[i].walk[skeletons[i].idle].Height;
                                g2.DrawImage(skeletons[i].walk[skeletons[i].idle], skeletons[i].dst, skeletons[i].src, GraphicsUnit.Pixel);
                            }
                            if (skeletons[i].attacked != 0)
                            {

                                skeletons[i].src.Width = skeletons[i].Attack[skeletons[i].attack].Width;
                                skeletons[i].src.Height = skeletons[i].Attack[skeletons[i].attack].Height;
                                g2.DrawImage(skeletons[i].Attack[skeletons[i].attack], skeletons[i].dst, skeletons[i].src, GraphicsUnit.Pixel);
                            }
                        }
                        else
                        {
                            if (skeletons[i].attacked == 0 && skeletons[i].hit == 0)
                            {
                                skeletons[i].src.Width = skeletons[i].walkBwd[skeletons[i].idle].Width;
                                skeletons[i].src.Height = skeletons[i].walkBwd[skeletons[i].idle].Height;
                                g2.DrawImage(skeletons[i].walkBwd[skeletons[i].idle], skeletons[i].dst, skeletons[i].src, GraphicsUnit.Pixel);
                            }
                            if (skeletons[i].attacked != 0)
                            {
                                skeletons[i].src.Width = skeletons[i].BwdAttack[skeletons[i].attack].Width;
                                skeletons[i].src.Height = skeletons[i].BwdAttack[skeletons[i].attack].Height;
                                g2.DrawImage(skeletons[i].BwdAttack[skeletons[i].attack], skeletons[i].dst, skeletons[i].src, GraphicsUnit.Pixel);

                            }
                        }
                    }
                    else
                    {
                        g2.DrawImage(skeletons[i].death[skeletons[i].dead], skeletons[i].dst, skeletons[i].src, GraphicsUnit.Pixel);
                    }
                }
                for (int i = 0; i < keys.Count; i++)
                {
                    if (keys[i].cnd == 1)
                    {
                        g2.DrawImage(keys[i].img[keys[i].curr], keys[i].dst, keys[i].src, GraphicsUnit.Pixel);
                    }
                }
            }
            if(gameOver==2) 
            {
                g2.DrawImage(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\winscreen.png"),0,0,Screen.GetWorkingArea(this).Width,Screen.GetWorkingArea(this).Height);
            }
            if (gameOver == 1)
            {
                g2.DrawImage(new Bitmap(@"C:\Users\omarn\source\repos\longGame\project assets\loseScreen.png"), 0, 0, Screen.GetWorkingArea(this).Width, Screen.GetWorkingArea(this).Height);
            }
        }

        public void drawDouble(Graphics g)
        {
            g2 = Graphics.FromImage(buffer);
            draw(g2);
            g.DrawImage(buffer,0,0);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
