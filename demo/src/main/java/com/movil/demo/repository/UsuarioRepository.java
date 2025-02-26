package com.movil.demo.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import com.movil.demo.model.*;
import java.util.Optional;

public interface UsuarioRepository extends JpaRepository<Usuario, Long> {

    Optional<Usuario> findByUsername(String username);
}
