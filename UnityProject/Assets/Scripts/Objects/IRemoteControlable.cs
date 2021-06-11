
public interface IRemoteControlable
{
    void PlayerTargetStart();
    void PlayerTargetExit();
    bool ChangeChannel(int channel, out bool disconnect);
}
