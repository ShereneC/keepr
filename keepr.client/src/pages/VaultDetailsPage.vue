<template>
  <div class="container">
    <div class="row">
      <div class="col-12 d-flex align-items-center justify-content-between">
        <h1>{{ vault.name }}</h1>
        <h5 class="fa fa-trash text-red"></h5>
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
        <div class="card-columns">
          <!-- TODO something happened when I put this in, things show up, but I am getting an error in the console and it breaks my links moving forward - something about cannot read property picture of null (keep.vue line 13)  but the picture is showing up!  Ah-ha! It is because getKeepsInVault in the VaultKeepsREpository is not populating - need to get that to populate!!!! -->
          <Keep v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'

export default {

  setup() {
    const route = useRoute()
    onMounted(async() => {
      try {
        await vaultsService.getVaultById(route.params.id)
        await keepsService.getKeepsByVaultId(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    }
    )
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      numberKeeps: computed(() => {
        return AppState.keeps.length
      })
    }
  }

}

</script>

<style lang="scss" scoped>

</style>
