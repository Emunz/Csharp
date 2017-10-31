namespace human_oop {
    public class Human {
        public string name;  
        public int strength;
        public int intelligence;
        public int dexterity;
        public int health;
        public Human(string val, int a = 3, int b = 3, int c = 3, int d = 100){
            name = val;
            strength = a;
            intelligence = b;
            dexterity = c;
            health = d;
        }

        public int Attack(Human player){
            if(player is Human){
                player.health -= (5 * strength);
                return player.health;
            } else {
                return player.health;
            } 
        }
    }
}