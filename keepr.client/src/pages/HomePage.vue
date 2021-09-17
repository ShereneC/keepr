<template>
  <div class="home flex-grow-1 container" v-if="account">
    <!-- <img src="https://bcw.blob.core.windows.net/public/img/8600856373152463" alt="CodeWorks Logo"> -->
    <div class="row">
      <div class="col-md-12">
        <div v-if="loading" class="col text-center">
          <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
        </div>
        <div class="card-columns" v-else>
          <Keep v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, computed, ref } from '@vue/runtime-core'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'

export default {
  setup() {
    const loading = ref(true)
    onMounted(async() => {
      try {
        await keepsService.getAllKeeps()
        loading.value = false
        await vaultsService.getVaultsByProfileId(AppState.account.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      loading,
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
