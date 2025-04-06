using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
    public class OtherPlayersManager
    {
        private SocketPackageNet mSocket;
        private Hashtable mOtherPlayers = new Hashtable();
        private Hashtable mHasInstanceOtherPlayers = new Hashtable();
        private List<OtherPlayer> mNeedRemovePlayers = new List<OtherPlayer>();
        private GameObject mOtherPlayerPrefab;
        private int mSelfUserId;

        static OtherPlayersManager(){
            ServerPackageManager.get().registerServerPackage(new OtherPlayersPositionPackage());
            ServerPackageManager.get().registerServerPackage(new OtherPlayersChantPackage());
        }


        public OtherPlayersManager()
        {
            OtherPlayersPackageListener otherPlayersPackageListener = new OtherPlayersPackageListener(this);
            otherPlayersPackageListener.subscribePackages(new OtherPlayersPositionPackage());
            otherPlayersPackageListener.subscribePackages(new OtherPlayersChantPackage());
            mSocket = NetworkFactory.getSocketNet();
            mSocket.addPackageListeners(otherPlayersPackageListener);
            mOtherPlayerPrefab = Resources.Load("Character/Player/OtherPlayerPrefabs/mini_pixel") as GameObject;
            mSelfUserId = int.Parse(UserRecorder.get().getUserId());
        }


        private class OtherPlayersPackageListener : PackageListener
        {
            OtherPlayersManager mOuter;
            public OtherPlayersPackageListener(OtherPlayersManager outer)
            {
                mOuter = outer;
            }

            public override void onReceive(ServerPackage package)
            {
                package.process(mOuter);
            }
        }

        private class OtherPlayersPositionPackage : ServerPackage
        {
            private int sceneId;
            private int otherPlayersCounts;
            private OtherPlayersPosition[] otherPlayersPosition;
            public OtherPlayersPositionPackage(ServerUnpackage unpackage)
            {
                byte[] body = unpackage.getBody();
                sceneId = BytesUtils.bytes2Int(BytesUtils.subbytes(body, 0, 4));
                otherPlayersCounts = BytesUtils.bytes2Int(BytesUtils.subbytes(body, 4, 8));
                otherPlayersPosition = new OtherPlayersPosition[otherPlayersCounts];
                for (int i=0; i< otherPlayersCounts; i++)
                {
                    int start = 8 + i * 16;
                    int playerId = BytesUtils.bytes2Int(BytesUtils.subbytes(body, start, start + 4));
                    Vector2 position = BytesUtils.bytes2Vector(BytesUtils.subbytes(body, start + 4, start + 12));
                    int direction = BytesUtils.bytes2Int(BytesUtils.subbytes(body, start + 12, start + 16));
                    otherPlayersPosition[i] = new OtherPlayersPosition(playerId, position, direction);
                }
            }

            public OtherPlayersPositionPackage() 
            {
                
            }

            public override ServerPackage create(ServerUnpackage unpackage)
            {
                return new OtherPlayersPositionPackage(unpackage);
            }

            public override string getHeaderString()
            {
                return "OtherPlayersPosition";
            }

            public override void process(object outer2)
            {
                OtherPlayersManager outer = outer2 as OtherPlayersManager;
                for (int i = 0; i < otherPlayersCounts; i++)
                {
                    OtherPlayersPosition pos = otherPlayersPosition[i];
                    int otherPlayerUserId = pos.playerId;
                    if (otherPlayerUserId != outer.mSelfUserId)
                    {
                        OtherPlayer otherPlayer = outer.mOtherPlayers[otherPlayerUserId] as OtherPlayer;
                        if (otherPlayer == null)
                        {
                            object hasInstance = outer.mHasInstanceOtherPlayers[otherPlayerUserId];
                            if (hasInstance == null || !(bool)hasInstance)
                            {
                                outer.mHasInstanceOtherPlayers.Add(otherPlayerUserId, true);
                                ThreadLooper.get().runMainThread(new Runable(() =>
                                {
                                    GameObject otherPlayerInstance = Object.Instantiate(outer.mOtherPlayerPrefab);
                                    otherPlayer = otherPlayerInstance.GetComponent<OtherPlayer>();
                                    outer.mOtherPlayers.Add(pos.playerId, otherPlayer);
                                    otherPlayer.whenInitialized(() =>
                                    {
                                        otherPlayer.initialize(pos.playerId, pos.position);
                                    });
                                }));
                            }
                        }
                        else
                        {
                            ThreadLooper.get().runMainThread(new Runable(() =>
                            {
                                otherPlayer.setPosition(pos.position, pos.direction);
                            }));
                        }
                    }
                }
                //检查其他玩家是否仍然在线
                outer.mNeedRemovePlayers.Clear();
                ICollection otherPlayers = outer.mOtherPlayers.Values;
                foreach (OtherPlayer other in otherPlayers)
                {
                    int lifetime = other.getLifetime();
                    if (lifetime == 0)
                    {
                        outer.mNeedRemovePlayers.Add(other);
                    }
                    else
                    {
                        other.setLifetime(0);
                    }
                }
                foreach (OtherPlayer other in outer.mNeedRemovePlayers)
                {
                    outer.mOtherPlayers.Remove(other.getPlayerId());
                    outer.mHasInstanceOtherPlayers.Remove(other.getPlayerId());
                    ThreadLooper.get().runMainThread(() =>
                    {
                        if(other != null)
                        {
                            other.destory();
                        }
                        
                    });
                }
            }
        }

        private class OtherPlayersPosition
        {
            public int playerId { get; }
            public Vector2 position { get; }
            public int direction { get; }

            public OtherPlayersPosition(int playerId, Vector2 position, int direction)
            {
                this.playerId = playerId;
                this.position = position;
                this.direction = direction;
            }
        }

        private class OtherPlayersChantPackage : ServerPackage
        {
            private int sceneId;
            private int otherPlayersCounts;
            private OtherPlayerChant[] otherPlayersChant;
            public OtherPlayersChantPackage(ServerUnpackage unpackage)
            {
                byte[] body = unpackage.getBody();
                sceneId = BytesUtils.bytes2Int(BytesUtils.subbytes(body, 0, 4));
                otherPlayersCounts = BytesUtils.bytes2Int(BytesUtils.subbytes(body, 4, 8));
                otherPlayersChant = new OtherPlayerChant[otherPlayersCounts];
                for (int i = 0; i < otherPlayersCounts; i++)
                {
                    int start = 8 + i * 12;
                    int playerId = BytesUtils.bytes2Int(BytesUtils.subbytes(body, start, start + 4));
                    int wordId = BytesUtils.bytes2Int(BytesUtils.subbytes(body, start + 4, start + 8));
                    int count = BytesUtils.bytes2Int(BytesUtils.subbytes(body, start + 8, start + 12));
                    otherPlayersChant[i] = new OtherPlayerChant(playerId, wordId, count);
                }
            }

            public OtherPlayersChantPackage() 
            {
                
            }

            public override ServerPackage create(ServerUnpackage unpackage)
            {
                return new OtherPlayersChantPackage(unpackage);
            }

            public override string getHeaderString()
            {
                return "OtherPlayersChant";
            }

            public override void process(object outer2)
            {
                OtherPlayersManager outer = outer2 as OtherPlayersManager;
                for (int i = 0; i < otherPlayersCounts; i++)
                {
                    OtherPlayerChant chant = otherPlayersChant[i];
                    int otherPlayerUserId = chant.playerId;
                    if (otherPlayerUserId != outer.mSelfUserId)
                    {
                        OtherPlayer otherPlayer = outer.mOtherPlayers[otherPlayerUserId] as OtherPlayer;
                        if (otherPlayer != null)
                        {
                            otherPlayer.addChant(chant.wordId, chant.count);
                        }
                    }
                }
            }
        }

        private class OtherPlayerChant
        {
            public int playerId { get; }
            public int wordId { get; }
            public int count { get; }

            public OtherPlayerChant(int playerId, int wordId, int count)
            {
                this.playerId = playerId;
                this.wordId = wordId;
                this.count = count;
            }
        }
    }
}
