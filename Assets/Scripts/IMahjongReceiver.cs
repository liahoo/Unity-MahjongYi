namespace MahjongGame.Controllers {
    public interface IMahjongReceiver {
        bool ReceiveMahjong(int value);
        bool ReceiveMahjong(Mahjong value);
    }
}