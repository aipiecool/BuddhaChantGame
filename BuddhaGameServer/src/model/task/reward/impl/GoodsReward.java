package model.task.reward.impl;

import model.good.entity.rom.Goods;
import model.good.GoodsManager;
import model.player.Player;
import model.task.entity.rom.TaskInfo;
import model.task.reward.TaskReward;

public class GoodsReward extends TaskReward {

    private Goods mGoods;

    public GoodsReward(TaskInfo.ChildTask.TaskReward taskRewardInfo) {
        super(taskRewardInfo);

        mGoods = GoodsManager.get().getGoodByName(taskRewardInfo.rewardObject);
    }

    @Override
    public String getRewardName() {
        return mGoods.getInfo().localName;
    }

    @Override
    public void award(Player player) {
        player.getRunningGoods().addGoods(mGoods, mRewardCount);
    }
}
