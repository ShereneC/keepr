<template>
  <div class="home flex-grow-1 container" v-if="account">
    <!-- <img src="https://bcw.blob.core.windows.net/public/img/8600856373152463" alt="CodeWorks Logo"> -->
    <div class="row">
      <div class="col-md-12">
        <div class="card-columns">
          <Keep v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, computed } from '@vue/runtime-core'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
export default {
  setup() {
    onMounted(async() => {
      try {
        logger.log(AppState.account)
        logger.log('hello from HomePage onMounted')
        await keepsService.getAllKeeps()
        await vaultsService.getVaultsByProfileId(AppState.account.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      // had to compute account to get id to show up, but shouldn't have to?
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps)
      // vaults: computed(() => AppState.vaults)
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
