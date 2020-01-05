using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SheepAndWolves
{
    public partial class Form1 : Form
    {
        List<List<PictureBox>> board;
        Color brown = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
        bool mode;      // true - PvP, false - PvE
        bool player;    // true - Sheep, false - Wolf
        bool game_finished;
        bool square_selected;
        bool step;  // true - ход овцы, false - ход волка
        int[] sheep;
        int[] wolf1, wolf2, wolf3, wolf4;
        int[] selected_position;
        List<int[]> animals_list;

        public Form1()
        {
            InitializeComponent();
            InitializeBoard();
            mode = true;
            sheep = new int[2];
            wolf1 = new int[2];
            wolf2 = new int[2];
            wolf3 = new int[2];
            wolf4 = new int[2];
            animals_list = new List<int[]>();
            animals_list.Add(sheep);
            animals_list.Add(wolf1);
            animals_list.Add(wolf2);
            animals_list.Add(wolf3);
            animals_list.Add(wolf4);
            //Restart();
        }

        private void restart_button_Click(object sender, EventArgs e)
        {
            Restart();
            if (!mode && !player)
            {
                Step(0, 0, -AB_VALUE, AB_VALUE);
                step = false;
            }
        }

        private void InitializeBoard()
        {
            board = new List<List<PictureBox>>();
            board.Add(null);
            board.Add(new List<PictureBox>());
            board[1].Add(null);
            board[1].Add(pictureBox11);
            board[1].Add(pictureBox12);
            board[1].Add(pictureBox13);
            board[1].Add(pictureBox14);
            board[1].Add(pictureBox15);
            board[1].Add(pictureBox16);
            board[1].Add(pictureBox17);
            board[1].Add(pictureBox18);
            board.Add(new List<PictureBox>());
            board[2].Add(null);
            board[2].Add(pictureBox21);
            board[2].Add(pictureBox22);
            board[2].Add(pictureBox23);
            board[2].Add(pictureBox24);
            board[2].Add(pictureBox25);
            board[2].Add(pictureBox26);
            board[2].Add(pictureBox27);
            board[2].Add(pictureBox28);
            board.Add(new List<PictureBox>());
            board[3].Add(null);
            board[3].Add(pictureBox31);
            board[3].Add(pictureBox32);
            board[3].Add(pictureBox33);
            board[3].Add(pictureBox34);
            board[3].Add(pictureBox35);
            board[3].Add(pictureBox36);
            board[3].Add(pictureBox37);
            board[3].Add(pictureBox38);
            board.Add(new List<PictureBox>());
            board[4].Add(null);
            board[4].Add(pictureBox41);
            board[4].Add(pictureBox42);
            board[4].Add(pictureBox43);
            board[4].Add(pictureBox44);
            board[4].Add(pictureBox45);
            board[4].Add(pictureBox46);
            board[4].Add(pictureBox47);
            board[4].Add(pictureBox48);
            board.Add(new List<PictureBox>());
            board[5].Add(null);
            board[5].Add(pictureBox51);
            board[5].Add(pictureBox52);
            board[5].Add(pictureBox53);
            board[5].Add(pictureBox54);
            board[5].Add(pictureBox55);
            board[5].Add(pictureBox56);
            board[5].Add(pictureBox57);
            board[5].Add(pictureBox58);
            board.Add(new List<PictureBox>());
            board[6].Add(null);
            board[6].Add(pictureBox61);
            board[6].Add(pictureBox62);
            board[6].Add(pictureBox63);
            board[6].Add(pictureBox64);
            board[6].Add(pictureBox65);
            board[6].Add(pictureBox66);
            board[6].Add(pictureBox67);
            board[6].Add(pictureBox68);
            board.Add(new List<PictureBox>());
            board[7].Add(null);
            board[7].Add(pictureBox71);
            board[7].Add(pictureBox72);
            board[7].Add(pictureBox73);
            board[7].Add(pictureBox74);
            board[7].Add(pictureBox75);
            board[7].Add(pictureBox76);
            board[7].Add(pictureBox77);
            board[7].Add(pictureBox78);
            board.Add(new List<PictureBox>());
            board[8].Add(null);
            board[8].Add(pictureBox81);
            board[8].Add(pictureBox82);
            board[8].Add(pictureBox83);
            board[8].Add(pictureBox84);
            board[8].Add(pictureBox85);
            board[8].Add(pictureBox86);
            board[8].Add(pictureBox87);
            board[8].Add(pictureBox88);
        }

        private void PvE_Click(object sender, EventArgs e)
        {
            game_finished = true;
            mode = false;
            SheepButton.Enabled = true;
            WolfButton.Enabled = true;
            PvPbutton.Enabled = true;
            PvEbutton.Enabled = false;
            searchWay = new Queue<int[]>();
            initMap();
            foreach (var lst in board)
            {
                if (lst != null)
                    foreach (PictureBox pb in lst)
                    {
                        if (pb != null)
                            pb.Refresh();
                    }
            }
        }

        private void PvP_Click(object sender, EventArgs e)
        {
            mode = true;
            SheepButton.Enabled = false;
            WolfButton.Enabled = false;
            PvPbutton.Enabled = false;
            PvEbutton.Enabled = true;
            Restart();
        }

        private void SheepButton_Click(object sender, EventArgs e)
        {
            player = true;
            Restart();
        }

        private void WolfButton_Click(object sender, EventArgs e)
        {
            player = false;
            Restart();
            Step(0, 0, -AB_VALUE, AB_VALUE);
            step = false;
        }

        private void Restart() {
            square_selected = false;
            game_finished = false;
            step = true;
            sheep[0] = 4;
            sheep[1] = 1;
            wolf1[0] = 1;
            wolf1[1] = 8;
            wolf2[0] = 3;
            wolf2[1] = 8;
            wolf3[0] = 5;
            wolf3[1] = 8;
            wolf4[0] = 7;
            wolf4[1] = 8;
            foreach (var lst in board)
            {
                if (lst != null)
                    foreach (PictureBox pb in lst)
                    {
                        if (pb != null)
                            pb.Refresh();
                    }
            }
            DrawAnimals();
        }

        private void DrawAnimals()
        {
            bool fl = true;
            foreach (int[] arr in animals_list)
            {
                //System.Diagnostics.Debug.WriteLine($"X: {arr[0]}    Y: {arr[1]}\n");
                PictureBox pictureBox = board[arr[0]][arr[1]];
                pictureBox.Refresh();
                Graphics graphics = pictureBox.CreateGraphics();
                if (fl)
                    graphics.FillEllipse(Brushes.Green, 15, 15, 20, 20);
                else
                    graphics.FillEllipse(Brushes.Blue, 15, 15, 20, 20);
                //pictureBox.Refresh();
                fl = false;
            }
        }

        private void DrawAnimal(PictureBox pictureBox, int animal, bool fill_square)
        {
            Graphics graphics = pictureBox.CreateGraphics();
            if (fill_square)
                graphics.FillRectangle(Brushes.Red, 0, 0, 50, 50);
            if (animal == 0)
                graphics.FillEllipse(Brushes.Green, 15, 15, 20, 20);
            else
                graphics.FillEllipse(Brushes.Blue, 15, 15, 20, 20);
        }

        private void DrawAnimal(int x, int y, int animal, bool fill_square)
        {
            Graphics graphics = board[x][y].CreateGraphics();
            if (fill_square)
                graphics.FillRectangle(Brushes.Red, 0, 0, 50, 50);
            if (animal == 0)
                graphics.FillEllipse(Brushes.Green, 15, 15, 20, 20);
            else
                graphics.FillEllipse(Brushes.Blue, 15, 15, 20, 20);
        }

        private int isAnimalPosition(PictureBox pb)
        {
            for (int i = 0; i < 5; i++)
                if (PositionX(pb) == animals_list[i][0]
                    && PositionY(pb) == animals_list[i][1])
                    return i;
            return -1;
        }

        private int isAnimalPosition(int x, int y)
        {
            for (int i = 0; i < 5; i++)
                if (x == animals_list[i][0] && y == animals_list[i][1])
                    return i;
            return -1;
        }

        private int PositionX(PictureBox pictureBox)
        {
            return pictureBox.Name[pictureBox.Name.Count() - 2] - '0';
        }

        private int PositionY(PictureBox pictureBox)
        {
            return pictureBox.Name.Last() - '0';
        }

        

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (System.Windows.Forms.PictureBox)sender;

            if (pictureBox.BackColor == brown && !game_finished)
            {
                int pos = isAnimalPosition(pictureBox);
                if (!square_selected)
                {
                    if ((pos == 0 && step) || (pos > 0 && !step))
                    {
                        square_selected = true;
                        selected_position = new int[3] { PositionX(pictureBox),
                            PositionY(pictureBox), pos};
                        DrawAnimal(pictureBox, pos, true);
                    }
                }
                else
                {
                    if (pos < 0)
                    {
                        int x = PositionX(pictureBox);
                        int y = PositionY(pictureBox);
                        if (isAllowedStep(selected_position[0], selected_position[1], x, y, selected_position[2]))
                        {
                            square_selected = false;
                            step = !step;
                            MoveAnimal(selected_position[0], selected_position[1], x, y, selected_position[2]);
                            if (!mode)
                            {
                                int animal = player ? 1 : 0;
                                Step(animal, 0, -AB_VALUE, AB_VALUE);
                                step = !step;
                                game_finished = isGameOver();
                            }
                        }
                    }
                    else
                    {
                        int x = PositionX(pictureBox);
                        int y = PositionY(pictureBox);
                        if (selected_position[2] > 0 && !(x == sheep[0] && y == sheep[1]))
                        {
                            board[selected_position[0]][selected_position[1]].Refresh();
                            DrawAnimal(selected_position[0], selected_position[1], selected_position[2], false);
                            selected_position = new int[3] { PositionX(pictureBox), PositionY(pictureBox), pos};
                            DrawAnimal(pictureBox, pos, true);
                        }
                    }
                }
            }
        }

        private bool isAllowedStep(int currX, int currY, int newX, int newY, int animal)
        {
            if (animal == 0)   // Bыбрана овца
            {
                if (isValidPosition(newX, newY)
                    && (currX == newX + 1 || currX == newX - 1)
                    && (currY == newY + 1 || currY == newY - 1))
                    return true;
            }
            else    // Выбран волк
            {
                if (isValidPosition(newX, newY)
                    && (currX == newX + 1 || currX == newX - 1)
                    && currY == newY + 1)
                    return true;
            }
            return false;
        }

        private bool isGameOver()
        {
            if (sheep[1] == 8) // Условие победы овцы
            {
                MessageBox.Show("Sheep won!");
                return true;
            }

            if (!isAllowedStep(sheep[0], sheep[1], sheep[0] + 1, sheep[1] + 1, 0)
             && !isAllowedStep(sheep[0], sheep[1], sheep[0] - 1, sheep[1] + 1, 0)
             && !isAllowedStep(sheep[0], sheep[1], sheep[0] + 1, sheep[1] - 1, 0)
             && !isAllowedStep(sheep[0], sheep[1], sheep[0] - 1, sheep[1] - 1, 0))
            {
                MessageBox.Show("Wolfs won!");
                return true;
            }
            return false;
        }

        private void MoveAnimal(int prevX, int prevY, int newX, int newY, int animal)
        {
            animals_list[animal][0] = newX;
            animals_list[animal][1] = newY;
            board[prevX][prevY].Refresh();
            DrawAnimal(newX, newY, animal, false);
            selected_position = null;
            game_finished = isGameOver();
        }

        Queue<int[]> searchWay;
        int[][] map;
        const int MAX_VALUE = 255;
        const int MIN_VALUE = 0;
        const int SHEEP = 1;
        const int WOLF = 255;
        const int EMPTY = 0;
        const int NOT_INIT = 255;
        const int AB_VALUE = 500;

        private void initMap()
        {
            map = new int[9][];
            map[0] = null;
            for (int i = 1; i < 9; i++)
            {
                map[i] = new int[9];
                map[i][0] = 255;
            }
        }

        private void prepareMap()
        {
            for (int i = 1; i < 9; i++)
                for (int j = 1; j < 9; j++)
                    map[i][j] = EMPTY;

            map[sheep[0]][sheep[1]] = SHEEP;
            for (int i = 1; i < 5; i++)
                map[animals_list[i][0]][animals_list[i][1]] = WOLF;
        }

        private void printMap()
        {
            for (int i = 8; i > 0; i--)
            {
                string s = $"\n {i}: ";
                for (int j = 1; j < 9; j++)
                    s += $"{map[j][i]} ";
                System.Diagnostics.Debug.WriteLine(s);
            }
        }

        private bool isValidPosition(int x, int y)
        {
            return isAnimalPosition(x, y) < 0 && x > 0 && x < 9 && y > 0 && y < 9;
        }

        private bool freePosition(int x, int y)
        {
            return isValidPosition(x, y) && map[x][y] == EMPTY;
        }

        private int GetHeuristic()
        {
            //printMap();
            //prepareMap();
            if (sheep[1] == 8)
                return 0;

            searchWay.Clear();
            searchWay.Enqueue(sheep);
            while (searchWay.Count > 0)
            {
                int[] currentPosition = searchWay.Dequeue();
                if (freePosition(currentPosition[0] - 1, currentPosition[1] + 1)){
                    int[] newPosition = { currentPosition[0] - 1, currentPosition[1] + 1 };
                    map[newPosition[0]][newPosition[1]] = map[currentPosition[0]][currentPosition[1]] + 1;
                    searchWay.Enqueue(newPosition);
                }
                if (freePosition(currentPosition[0] + 1, currentPosition[1] + 1))
                {
                    int[] newPosition = { currentPosition[0] + 1, currentPosition[1] + 1 };
                    map[newPosition[0]][newPosition[1]] = map[currentPosition[0]][currentPosition[1]] + 1;
                    searchWay.Enqueue(newPosition);
                }
                if (freePosition(currentPosition[0] - 1, currentPosition[1] - 1))
                {
                    int[] newPosition = { currentPosition[0] - 1, currentPosition[1] - 1 };
                    map[newPosition[0]][newPosition[1]] = map[currentPosition[0]][currentPosition[1]] + 1;
                    searchWay.Enqueue(newPosition);
                }
                if (freePosition(currentPosition[0] + 1, currentPosition[1] - 1))
                {
                    int[] newPosition = { currentPosition[0] + 1, currentPosition[1] - 1 };
                    map[newPosition[0]][newPosition[1]] = map[currentPosition[0]][currentPosition[1]] + 1;
                    searchWay.Enqueue(newPosition);
                }
            }
            
            int min = MAX_VALUE;
            for (int i = 0; i < 4; i++)
                if (map[i * 2 + 1][8] > MIN_VALUE && map[i * 2 + 1][8] < min)
                    min = map[i * 2 + 1][8];

            return min - 1;
        }


        void dummyMove(int x, int y, int animal)
        {
            if (animal == 0)
            {
                map[sheep[0]][sheep[1]] = EMPTY;
                map[sheep[0] + x][sheep[1] + y] = SHEEP;
                sheep[0] += x;
                sheep[1] += y;
            }
            else
            {
                map[animals_list[animal][0]][animals_list[animal][1]] = EMPTY;
                map[animals_list[animal][0] + x][animals_list[animal][1] + y] = WOLF;
                animals_list[animal][0] += x;
                animals_list[animal][1] += y;
            }
        }

        int Step(int animal, int recDepth, int alpha, int beta)
        {
            if (recDepth == 0)
                prepareMap();

            int test = NOT_INIT;

            if (recDepth >= 6)  // последний уровень рекурсии
            {
                int h = GetHeuristic();
                prepareMap();
                return h;
            }

            int way = NOT_INIT;     // 0-7 - ходы волков, 8-11 - ходы овцы

            bool isWolf = animal > 0;
            int res = isWolf ? MIN_VALUE : MAX_VALUE;

            for (int i = (isWolf ? 0 : 8); i < (isWolf ? 8 : 12); i++)
            {
                int curAnimal = isWolf ? i / 2 + 1 : 0;
                int curPosX = animals_list[curAnimal][0];
                int curPosY = animals_list[curAnimal][1];
                int curMoveX = NOT_INIT;
                int curMoveY = NOT_INIT;
                if (isWolf)
                {
                    if (i % 2 == 0)
                    {
                        curMoveX = -1;
                        curMoveY = -1;
                    }
                    else
                    {
                        curMoveX = 1;
                        curMoveY = -1;
                    }
                }
                else
                {
                    if (i % 4 == 0)
                    {
                        curMoveX = -1;
                        curMoveY = -1;
                    }
                    if (i % 4 == 1)
                    {
                        curMoveX = 1;
                        curMoveY = -1;
                    }
                    if (i % 4 == 2)
                    {
                        curMoveX = -1;
                        curMoveY = 1;
                    }
                    if (i % 4 == 3)
                    {
                        curMoveX = 1;
                        curMoveY = 1;
                    }
                }
                
                if (freePosition(curPosX + curMoveX, curPosY + curMoveY))
                {
                    dummyMove(curMoveX, curMoveY, curAnimal);
                    test = Step(isWolf ? 0 : 1, recDepth + 1, alpha, beta);
                    dummyMove(-curMoveX, -curMoveY, curAnimal);

                    if ((test > res && animal > 0) || (test <= res && animal == 0) || way == NOT_INIT)
                    {
                        res = test;
                        way = i;
                    }

                    if (isWolf)
                        alpha = alpha > test ? alpha : test;
                    else
                        beta = beta < test ? beta : test;

                    if (beta < alpha)
                        break;
                } 
            }
            if (way == NOT_INIT)
            {
                int h = GetHeuristic();
                prepareMap();
                return h;
            }

            if (recDepth == 0 && way != NOT_INIT)
            {
                if (animal > 0)
                {
                    int newX = NOT_INIT;
                    int newY = NOT_INIT;
                    if (way % 2 == 0)
                    {
                        newX = -1;
                        newY = -1;
                    }
                    else
                    {
                        newX = 1;
                        newY = -1;
                    }
                    int g = (way / 2) + 1;
                    board[animals_list[g][0]][animals_list[g][1]].Refresh();
                    animals_list[g][0] += newX;
                    animals_list[g][1] += newY;
                    DrawAnimal(animals_list[g][0], animals_list[g][1], animal, false);
                }
                else
                {
                    int newX = NOT_INIT;
                    int newY = NOT_INIT;
                    if (way % 4 == 0)
                    {
                        newX = -1;
                        newY = -1;
                    }
                    if (way % 4 == 1)
                    {
                        newX = 1;
                        newY = -1;
                    }
                    if (way % 4 == 2)
                    {
                        newX = -1;
                        newY = 1;
                    }
                    if (way % 4 == 3)
                    {
                        newX = 1;
                        newY = 1;
                    }
                    board[sheep[0]][sheep[1]].Refresh();
                    sheep[0] += newX;
                    sheep[1] += newY;
                    DrawAnimal(sheep[0], sheep[1], animal, false);
                }
            }

            return res;
        }
    }
}
