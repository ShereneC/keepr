<template>
  <div class="container">
    <div class="row">
      <div class="col-12 d-flex align-items-center justify-content-between">
        <h1>{{ vault.name }}</h1>
        <h5 class="m-0 hoverable" v-if="account.id==vault.creatorId" :title="'Delete ' + vault.name" @click="deleteVault">
          <span class="fa fa-trash text-red"></span>
        </h5>
      </div>
      <div class="col-12">
        <h4>{{ vault.description }}</h4>
        <h6 class="text-red" v-if="vault.isPrivate == true">
          Private Vault
        </h6>
        <h6 class="text-green" v-else>
          Public Vault
        </h6>
      </div>
      <div class="col-12">
        <h4>Keeps: {{ numberKeeps }}</h4>
      </div>
      <div class="col-12">
        <div v-if="loading" class="col text-center">
          <i class="fa fa-spinner fa-spin" aria-hidden="true"></i>
        </div>
        <div class="card-columns">
          <Keep v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from '@vue/runtime-core'
import { useRoute, useRouter } from 'vue-router'

import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'

export default {

  setup() {
    const loading = ref(true)
    const route = useRoute()
    const router = useRouter()
    // const goHome = ref(false)
    onMounted(async() => {
      try {
        await vaultsService.getVaultById(route.params.id)
        // if (res.isPrivate === true && res.creatorId !== account.id) {
        //   route.push({ name: 'Home' })
        // }
        await keepsService.getKeepsByVaultId(route.params.id)
        loading.value = false
      } catch (error) {
        Pop.toast(error, 'error')
        router.push({ name: 'Home' })
      }
    }
    )
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      vaultkeeps: computed(() => AppState.vaultkeeps),
      numberKeeps: computed(() => {
        return AppState.keeps.length
      }),
      async deleteVault() {
        try {
          if (await Pop.confirm()) {
            await vaultsService.deleteVault(route.params.id)
            Pop.toast('Successfully Deleted!', 'success')
            // NOTE hey! this works - totally awesome!
            router.go(-1)
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }

    }
  }

}

</script>

<style lang="scss" scoped>

</style>
